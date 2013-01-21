/// <reference path="..\breeze.debug.js" />

(function (root) {
    var app = root.app;
    var breeze = root.breeze,
        ko = root.ko,
        logger = app.logger,
        models = app.models,
        entityModel = breeze.entityModel;

    // define Breeze namespace
    function initManager() {

        // service name is route to the Web API controller
        var serviceName = (urlroot || '/') + 'api/WorkOrders';

        // manager is the service gateway and cache holder
        var manager = new entityModel.EntityManager(serviceName);

        manager.metadataStore.fetchMetadata(serviceName)
            .then(function () {
                //vm.metadata = manager.metadataStore.getEntityTypes();
            });
        manager.metadataStore.registerEntityTypeCtor("WorkOrder", function () { }, models.WorkOrder);
        manager.metadataStore.registerEntityTypeCtor("WorkItem", function () { }, models.WorkItem);
        return manager;
    }

    var manager = initManager();
    
    // define the viewmodel
    var vm = {
        workOrders: ko.observableArray(),
        SelectedWorkOrder: ko.observable(),
        includeDone: ko.observable(false),
        addWorkOrder: createNewWorkOrder,
        selectWorkOrder: selectWorkOrder,
        closeSelectedOrder: closeSelectedOrder,
        save: saveChanges,
        show: ko.observable(false),
        manager: manager,
        addWorkItem: addWorkItem,
        removeWorkItem: removeWorkItem,
        itemPriceSum: itemPriceSum,
        saveSelected: saveSelected,
        formatCurrency: app.helpers.formatCurrency,
        refresh: getWorkOrders,
        language: root.app.language,
        languages: root.app.languages
    };
    vm.workOrders.subscribe(function () {
        if (!vm.SelectedWorkOrder() && vm.workOrders().length > 0) {
            vm.SelectedWorkOrder(vm.workOrders()[0]);
        }
    });

    root.app.vm = vm;

    // start fetching Work Orders
    getWorkOrders();

    // re-query when "includeDone" checkbox changes
    //vm.includeDone.subscribe(getWorkOrders);

    // bind view to the viewmodel
    $(document).ready(function () {
        $('.datepicker').datepicker();
        ko.applyBindings(vm);
    });

    /* Private functions */

    // get Todos asynchronously
    // returning a promise to wait for     
    function getWorkOrders() {
        if (!manager) {
            manager = initManager();
        }
        logger.info("querying WorkOrders");

        var query = entityModel.EntityQuery.from("WorkOrders")
            .take(20)
            .expand("WorkItems, Vehicle, Customer");

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
            manager.rejectChanges();
            var workOrders = data.results;
            workOrders.forEach(function (workOrder) {
                vm.workOrders.push(workOrder);
            });
            vm.show(true); // show the view
        }
    };

    function createEntity(name) {
        return vm.manager.metadataStore.getEntityType(name).createEntity();
    }
    
    function createNewWorkOrder() {
        var wo = createEntity("WorkOrder");
        manager.addEntity(wo);
        vm.workOrders.push(wo);
        selectWorkOrder(wo);
        //wo.Customer(createEntity("Customer"));
        //wo.Vehicle(createEntity("Vehicle"));
        vm.save();
        logger.success("work order created");
    }

    function selectWorkOrder(workOrder) {
        vm.SelectedWorkOrder(workOrder);
        logger.success("work order {number} selected".assign({ number: workOrder.Number() }));
    }

    vm.SelectedWorkOrder.subscribe(function (workOrder) {
        if (!workOrder.Customer()) {
            workOrder.Customer(createEntity("Customer"));
        }
        if (!workOrder.Vehicle()) {
            workOrder.Vehicle(createEntity("Vehicle"));
        }
    });


    function addWorkItem(target, event) {
        var workItem = createEntity("WorkItem");
        vm.manager.addEntity(workItem);
        workItem.Type(target.type);
        vm.SelectedWorkOrder().WorkItems.push(workItem);
        //workItem[target.navigationProperty.inverse.name](target.parentEntity);
        //target.push(workItem);
    };

    function removeWorkItem(target) {
        target.entityAspect.setDeleted();
    };

    function itemPriceSum(items) {
        var sum = 0.0;
        $(items).each(function (index, item) {
            sum += item.Value();
        });
        return vm.formatCurrency(sum);
    }

    function closeSelectedOrder(order) {
        logger.warning("Sorry, not yet implemented!")
    }

    function saveChanges() {
        return manager.saveChanges()
            .then(function () { logger.success("changes saved"); })
            .fail(saveFailed);
    }

    function saveSelected() {
        return manager.saveChanges([vm.SelectedWorkOrder()])
            .then(function () { logger.success("changes saved"); })
            .fail(saveFailed);;
    }

    function queryFailed(error) {
        logger.error("Query failed: " + error.message);
    }

    function saveFailed(error) {
        logger.error("Save failed: " + error.message);
    }

}(window));