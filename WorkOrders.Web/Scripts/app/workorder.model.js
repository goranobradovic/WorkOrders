(function (ko, app) {
    app.models = app.models || {};
    app.models.WorkItem = WorkItem;
    app.models.WorkOrder = WorkOrder;
    var formatCurrency = app.helpers.formatCurrency;

    function WorkItem(data) {
        var self = data;

        // Persisted properties
        //self.WorkItemId = data.WorkItemId;
        //self.Title = ko.observable(data.Title);
        //self.IsDone = ko.observable(data.IsDone);
        //self.WorkOrderId = data.WorkOrderId;

        // Non-persisted properties
        self.ErrorMessage = ko.observable();

        if (!self.Amount()) {
            self.Amount(1);
        }

        self.Amount.subscribe(function (value) {
            if (value <= 0) {
                self.entityAspect.setDeleted();
            }
        });

        self.FormattedValue = self.Value.extend({ numeric: 2 });

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
        var self = data;
        //data = data || {};

        // Persisted properties
        //self.Id = data.Id || ' ';
        //self.Advice = data.Advice || '';
        //self.Approved = ko.observable(data.Approved || false);
        //self.ApprovedMaxValue = data.ApprovedMaxValue || false;
        //self.Customer = new Customer(data.Customer);
        //self.CustomerId = ko.observable(data.CustomerId);
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
        if (!self.DateReceived()) {
            self.DateReceived(new Date());
        }

        //self.WorkOrdered = ko.observableArray([]);

        // Non-persisted properties

        self.ErrorMessage = ko.observable();
        //self.Approved = ko.observable();
        //self.RequestForEstimate = ko.observable();
        self.WorkItemsFiltered = function (type) {
            return ko.dependentObservable(function () {
                var items = self.WorkItems().filter(function(item) { return item.Type() == type; });
                return {
                    items: items,
                    sum: app.vm.itemPriceSum(items),
                    type: type,
                    add: app.vm.addWorkItem
                };
            });
        }.bind(self);

        self.WorkOrdered = ko.computed(function () {
            return self.WorkItems().filter(function (item) { return item.Type() == 1; });
        });
        self.WorkOrdered.type = "WorkOrdered";
        self.WorkNeeded = ko.computed(function () {
            return self.WorkItems().filter(function (item) { return item.Type() == 2; });
        });
        self.WorkPerformed = ko.computed(function () {
            return self.WorkItems().filter(function (item) { return item.Type() == 3; });
        });
        self.PartsInstalled = ko.computed(function () {
            return self.WorkItems().filter(function (item) { return item.Type() == 4; });
        });
        self.WorkPerformedSum = ko.computed(function () {
            return app.vm.itemPriceSum(self.WorkPerformed());
        });
        self.PartsInstalledSum = ko.computed(function () {
            return app.vm.itemPriceSum(self.PartsInstalled());
        });
        self.TotalSum = ko.computed(function () {
            return formatCurrency(parseFloat(self.WorkPerformedSum()) + parseFloat(self.PartsInstalledSum()));
        });

        ko.computed(function () {
            if (self.Approved()) {
                self.ApprovedMax(false);
                self.RequestForEstimate(false);
            }
        });

        ko.computed(function () {
            if (self.ApprovedMax() || self.RequestForEstimate()) {
                self.Approved(false);
            }
        });

        ko.computed(function () {
            if (self.ApprovedMax() && !self.ApprovedMaxValue()) {
                self.ApprovedMaxValue(100.0);
            }
        });


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
