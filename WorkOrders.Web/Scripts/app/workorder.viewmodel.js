/// <reference path="..\breeze.debug.js" />

(function (root) {
    var app = root.app;
    var breeze = root.breeze,
        ko = root.ko,
        logger = app.logger,
        models = app.models;

    // define Breeze namespace
    var entityModel = breeze.entityModel;

    // service name is route to the Web API controller
    var serviceName = 'api/WorkOrders';

    // manager is the service gateway and cache holder
    var manager = new entityModel.EntityManager(serviceName);

    manager.metadataStore.fetchMetadata(serviceName)
        .then(function () {

            var metadata = {
                WorkOrder: manager.metadataStore.getEntityType("WorkOrder"),
                WorkItem: manager.metadataStore.getEntityType("WorkItem"),
                Vehicle: manager.metadataStore.getEntityType("Vehicle"),
                Client: manager.metadataStore.getEntityType("Client")
            };
            vm.metadata = metadata;
        });
    manager.metadataStore.registerEntityTypeCtor("WorkOrder", function () { }, models.WorkOrder);
    manager.metadataStore.registerEntityTypeCtor("WorkItem", function () { }, models.WorkItem);

    // define the viewmodel
    var vm = {
        workOrders: ko.observableArray(),
        selectedWorkOrder: ko.observable(),
        includeDone: ko.observable(false),
        addWorkOrder: createNewWorkOrder,
        selectWorkOrder: selectWorkOrder,
        save: saveChanges,
        show: ko.observable(false),
        manager: manager,
        addWorkItem: addWorkItem,
        removeWorkItem: removeWorkItem,
        itemPriceSum: itemPriceSum,
        formatCurrency: app.helpers.formatCurrency
    };
    vm.workOrders.subscribe(function () {
        if (!vm.selectedWorkOrder() && vm.workOrders().length > 0) {
            vm.selectedWorkOrder(vm.workOrders()[0]);
        }
    });

    root.app.vm = vm;

    // start fetching Work Orders
    getWorkOrders();

    // re-query when "includeDone" checkbox changes
    //vm.includeDone.subscribe(getWorkOrders);

    // bind view to the viewmodel
    ko.applyBindings(vm);

    /* Private functions */

    // get Todos asynchronously
    // returning a promise to wait for     
    function getWorkOrders() {

        logger.info("querying WorkOrders");

        var query = entityModel.EntityQuery.from("WorkOrders");

        if (!vm.includeDone()) {
            query = query.where("CompletionDate", "==", null);
        }

        return manager
            .executeQuery(query)
            .then(querySucceeded)
            .fail(queryFailed);

        // clear observable array and load the results 
        function querySucceeded(data) {
            logger.success("queried WorkOrders");
            vm.workOrders.removeAll();
            var workOrders = data.results;
            workOrders.forEach(function (workOrder) {
                vm.workOrders.push(workOrder);
            });
            vm.show(true); // show the view
        }
    };

    function createNewWorkOrder() {
        var wo = vm.metadata.WorkOrder.createEntity();
        wo.Client(vm.metadata.Client.createEntity());
        wo.Vehicle(vm.metadata.Vehicle.createEntity());
        wo.Number(0);
        manager.addEntity(wo);
        vm.workOrders.push(wo);
        logger.success("work order created");
    }

    function selectWorkOrder(workOrder) {
        vm.selectedWorkOrder(workOrder);
        logger.success("work order {number} selected".assign({ number: workOrder.Number() }));
    }


    function addWorkItem(target, event) {
        var workItem = vm.metadata.WorkItem.createEntity();
        vm.manager.addEntity(workItem);
        target.push(workItem);
    };

    function removeWorkItem(target) {
        target.entityAspect.setDeleted();
    };

    function itemPriceSum(items) {
        var sum = 0.0;
        items.each(function (item, index) {
            sum += item.Value();
        });
        return vm.formatCurrency(sum);
    }

    function saveChanges() {
        return manager.saveChanges()
            .then(function () { logger.success("changes saved"); })
            .fail(saveFailed);
    }

    function queryFailed(error) {
        logger.error("Query failed: " + error.message);
    }

    function saveFailed(error) {
        logger.error("Save failed: " + error.message);
    }

}(window));