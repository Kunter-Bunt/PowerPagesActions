"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.actions = void 0;
var actions;
(function (actions) {
    function callAction(operation, inputs, onSuccess, onError) {
        let actionInputs = null;
        if (typeof inputs === 'string' || inputs instanceof String)
            actionInputs = inputs;
        else if (typeof inputs === 'object' && inputs !== null)
            actionInputs = JSON.stringify(inputs);
        let url = `/_api/mwo_powerpagesactions?$filter=createdon ge ${new Date().toISOString()} and mwo_operation eq '${operation}'`;
        if (actionInputs)
            url += ` and mwo_inputs eq '${actionInputs}'`;
        safeAjax({
            type: "GET",
            url: url,
            success: (data) => successFunction(data, onSuccess, onError),
            error: (jqXHR) => errorFunction(jqXHR, onError)
        });
    }
    actions.callAction = callAction;
    actions.callAction = callAction;
    function successFunction(data, onSuccess, onError) {
        console.log(data);
        var record = data.value[0];
        var outputs = JSON.parse(record.mwo_outputs);
        if (record.statuscode == 407770001)
            onSuccess(outputs);
        else
            onError(outputs);
    }
    function errorFunction(jqXHR, onError) {
        console.log(jqXHR);
        if (jqXHR.status == 404)
            onError({ message: "Actions endpoint not found, please check whether you have configured the Site Settings 'Webapi/mwo_powerpagesaction/enabled: True' and 'Webapi/mwo_powerpagesaction/fields: *'" });
        else
            onError(jqXHR.responseJSON);
    }
    function safeAjax(ajaxOptions) {
        var deferredAjax = $.Deferred();
        shell.getTokenDeferred().done(function (token) {
            if (!ajaxOptions.headers) {
                $.extend(ajaxOptions, {
                    headers: {
                        "__RequestVerificationToken": token
                    }
                });
            }
            else {
                ajaxOptions.headers["__RequestVerificationToken"] = token;
            }
            $.ajax(ajaxOptions)
                .done(function (data, textStatus, jqXHR) {
                validateLoginSession(data, textStatus, jqXHR, deferredAjax.resolve);
            }).fail(deferredAjax.reject);
        }).fail(function () {
            deferredAjax.rejectWith(this, arguments);
        });
        return deferredAjax.promise();
    }
})(actions = exports.actions || (exports.actions = {}));
//# sourceMappingURL=actions.js.map