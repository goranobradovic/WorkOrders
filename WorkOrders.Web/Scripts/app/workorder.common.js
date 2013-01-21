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
            read: function () { return formatCurrency(target()); },  //always return the original observables value
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

    root.ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor) {
            $(element).datepicker()
                .on('changeDate', function (ev) {
                    var value = valueAccessor();
                    value(ev.date.valueOf());
                });
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            if (value()) {
                $(element).datepicker('setValue', value());
            }
            else {
                $(element).val('');
            }
        }
    };


    function updateLocalizations(resources) {
        var supportedAttributes = ['text', 'title'];
        for (var i in supportedAttributes) {
            var attr = supportedAttributes[i];
            $("[data-for-" + attr + "]").each(function () {
                var field = $(this);
                var key = field.data("for-" + attr);
                if (resources[key]) {
                    if (attr == "text") {
                        field.text(resources[key]);
                    }
                    else {
                        field.attr(attr, resources[key]);
                    }
                }
            });
        }
    }

    root.app.language = ko.observable(culture);
    root.app.languages = [
        { code: 'en', flag: 'gb', name: 'English' },
        { code: 'de', flag: 'de', name: 'Deutsch' },
        { code: 'el', flag: 'gr', name: 'ελληνικά' },
        { code: 'es', flag: 'es', name: 'español' },
        { code: 'fi', flag: 'fi', name: 'suomi' },
        { code: 'fr', flag: 'fr', name: 'français' },
        { code: 'ga', flag: 'ga', name: 'Gaeilge' },
        { code: 'hr', flag: 'hr', name: 'Hrvatski' },
        { code: 'it', flag: 'it', name: 'italiano' },
        { code: 'ja', flag: 'jp', name: '日本人' },
        { code: 'mk', flag: 'mk', name: 'македонски' },
        { code: 'ru', flag: 'ru', name: 'русский' },
        { code: 'sl', flag: 'sl', name: 'Slovenski' },
        { code: 'sr', flag: 'rs', name: 'Srpski' }];
    root.app.resources = ko.observable({});
    root.app.resources.subscribe(updateLocalizations);
    root.app.language.subscribe(function (language) {
        $.getJSON((urlroot || '/') + 'Scripts/Resources/' + language)
            .done(function (data) {
                root.app.resources(data);
            });
    });

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
