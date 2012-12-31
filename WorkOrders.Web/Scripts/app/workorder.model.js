(function (ko, datacontext) {

    datacontext.WorkItem = WorkItem;
    datacontext.WorkOrder = WorkOrder;

    function WorkItem(data) {
        var self = this;
        data = data || {};

        // Persisted properties
        self.WorkItemId = data.WorkItemId;
        self.Title = ko.observable(data.Title);
        self.IsDone = ko.observable(data.IsDone);
        self.WorkOrderId = data.WorkOrderId;

        // Non-persisted properties
        self.ErrorMessage = ko.observable();

        self.save = function () { return datacontext.saveChangedWorkItem(self); };

        // Auto-save when these properties change
        self.IsDone.subscribe(self.save);
        self.Title.subscribe(self.save);
    };

    function WorkOrder(data) {
        var self = this;
        data = data || {};

        // Persisted properties
        self.WorkOrderId = data.WorkOrderId;
        self.UserId = data.UserId || "to be replaced";
        self.Title = ko.observable(data.Title || "My WorkOrders");
        self.WorkOrders = ko.observableArray(importWorkItems(data.WorkOrders));

        // Non-persisted properties
        self.IsEditingListTitle = ko.observable(false);
        self.NewWorkOrderTitle = ko.observable();
        self.ErrorMessage = ko.observable();

        self.save = function () { return datacontext.saveChangedWorkOrder(self); };
        self.deleteWorkOrder = function () {
            var WorkItem = this;
            return datacontext.deleteWorkItem(WorkItem)
                 .done(function () { self.WorkOrders.remove(WorkItem); });
        };

        // Auto-save when these properties change
        self.Title.subscribe(self.save);

    };
    // convert raw WorkItem data objects into array of WorkItems
    function importWorkItems(WorkItems) {
        return $.map(WorkItems || [],
                function (WorkItemData) {
                    return datacontext.createWorkItem(WorkItemData);
                });
    }
    WorkOrder.prototype.addWorkOrder = function () {
        var self = this;
        if (self.NewWorkOrderTitle()) { // need a title to save
            var WorkItem = datacontext.createWorkItem(
                {
                    Title: self.NewWorkOrderTitle(),
                    WorkOrderId: self.WorkOrderId
                });
            self.WorkOrders.push(WorkItem);
            datacontext.saveNewWorkItem(WorkItem);
            self.NewWorkOrderTitle("");
        }
    };

})(ko, workOrderApp.datacontext);
