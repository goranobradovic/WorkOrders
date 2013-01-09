(function (root) {
    root.app.helpers = root.app.helpers || {};
    root.app.helpers.formatCurrency = formatCurrency;
    function formatCurrency(value, precision) {
        if (value && value.toFixed) {
            return value.toFixed(precision || 2);
        }
        return (0).toFixed(precision || 2);
    };

    root.ko.extenders.numeric = function (target, precision) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: function () { return formatCurrency(target()) },  //always return the original observables value
            write: function (newValue) {
                var current = target(),
                    roundingMultiplier = Math.pow(10, precision),
                    newValueAsNum = isNaN(newValue) ? 0 : parseFloat(+newValue),
                    valueToWrite = formatCurrency(Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier, precision);

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

}(window));

(function (root) {
    root.app.autocomplete = root.app.autocomplete || {};
    var data = {
        Manufacturers: ["Audi", "BMW", "Citroen", "Fiat", "Hyndai", "Jeep", "Lancia", "Mercedes", "Nissan", "Opel", "Zastava"]
    };
    var ac = root.app.autocomplete;

    ac.Manufacturers = function (query, process) {
        process(data.Manufacturers);
    };
}(window));
