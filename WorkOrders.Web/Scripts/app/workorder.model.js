(function (ko, app) {
    app.models = app.models || {};
    app.models.WorkItem = WorkItem;
    app.models.WorkOrder = WorkOrder;

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

        //self.save = function () { return datacontext.saveChangedWorkItem(self); };

        //// Auto-save when these properties change
        //self.IsDone.subscribe(self.save);
        //self.Title.subscribe(self.save);
    };

    function Client(data) {
        var self = this;

        return self;
    }

    function Vehicle(data) {
        var self = this;
        return self;
    }

    function WorkOrder(data) {
        var self = this;
        data = data || {};

        // Persisted properties
        //self.Id = data.Id || ' ';
        //self.Advice = data.Advice || '';
        //self.Approved = ko.observable(data.Approved || false);
        //self.ApprovedMaxValue = data.ApprovedMaxValue || false;
        //self.Client = new Client(data.Client);
        //self.ClientId = ko.observable(data.ClientId);
        //self.CompletionDate = data.CompletionDate;
        //self.CreatedBy = data.CreatedBy;
        //self.DateModified = data.DateModified;
        //self.DateReceived = data.DateReceived;
        //self.Deadline = data.Deadline;
        //self.DeliveredTo = data.DeliveredTo;
        //self.Employee = data.Employee;
        //self.EstimatedValue = data.EstimatedValue;
        //self.Number = data.Number;
        //self.RequestForEstimate = data.RequestForEstimate;
        //self.Vehicle = new Vehicle(data.Vehicle);
        //self.VehicleId = data.VehicleId;


        // Non-persisted properties

        self.ErrorMessage = ko.observable();

        self.save = function () { return datacontext.saveChangedWorkOrder(self); };
        self.deleteWorkOrder = function () {
            var WorkItem = this;
            //return datacontext.deleteWorkItem(WorkItem)
            //     .done(function () { self.WorkOrders.remove(WorkItem); });
        };

        // Auto-save when these properties change
        //self.Title.subscribe(self.save);

    };
    
    // convert raw WorkItem data objects into array of WorkItems
    function importWorkItems(WorkItems) {
        return $.map(WorkItems || [],
                function (WorkItemData) {
                    //return datacontext.createWorkItem(WorkItemData);
                });
    }
    
    WorkOrder.prototype.addWorkOrder = function () {
        var self = this;
        //if (self.NewWorkOrderTitle()) { // need a title to save
        //    var WorkItem = datacontext.createWorkItem(
        //        {
        //            Title: self.NewWorkOrderTitle(),
        //            WorkOrderId: self.WorkOrderId
        //        });
        //    self.WorkOrders.push(WorkItem);
        //    datacontext.saveNewWorkItem(WorkItem);
        //    self.NewWorkOrderTitle("");
        //}
    };

})(ko, window.app);
