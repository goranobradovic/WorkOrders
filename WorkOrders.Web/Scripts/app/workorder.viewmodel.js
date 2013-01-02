/// <reference path="..\breeze.debug.js" />

(function (root) {
    var breeze = root.breeze,
        ko = root.ko,
        logger = root.app.logger;

    // define Breeze namespace
    var entityModel = breeze.entityModel;

    // service name is route to the Web API controller
    var serviceName = 'api/WorkOrders';

    // manager is the service gateway and cache holder
    var manager = new entityModel.EntityManager(serviceName);

    // define the viewmodel
    var vm = {
        workOrders : ko.observableArray(),
        todos: ko.observableArray(),
        includeDone: ko.observable(false),
        save: saveChanges,
        show: ko.observable(false)
    };
    root.vm = vm;
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