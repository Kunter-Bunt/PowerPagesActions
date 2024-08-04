# PowerPagesCustomAPI
Call custom code from Power Pages! This page creates a mapping from a Custom API to the Power Pages OData Endpoint, allowing you to call selected Custom APIs from your website code.

## Guides
- Setup [![Setup](https://img.youtube.com/vi/oTJVEFKjM1Y/0.jpg)](https://www.youtube.com/watch?v=oTJVEFKjM1Y)
- Authorization [![Authorization](https://img.youtube.com/vi/l9CJR_pQ2T0/0.jpg)](https://www.youtube.com/watch?v=l9CJR_pQ2T0)
- Bound APIs [![Setup Guide](https://img.youtube.com/vi/2Q7ox1fyci0/0.jpg)](https://www.youtube.com/watch?v=2Q7ox1fyci0)
- All Parameter Types [![All Parameter Types](http://localhost:1313/post/power-pages/custom-api-parameters/cover.jpg)](https://www.marius-wodtke.de/post/power-pages/custom-api-parameters/)

## Setup
1. Install Managed Solution
2. Create Table Permission on `mwo_powerpagesaction`, Global Access, Read. Select the appropriate WebRoles, for more information regarding Security, see below.
3. Create 2 Site Settings
    - Webapi/mwo_powerpagesaction/enabled : True
    - Webapi/mwo_powerpagesaction/fields : *
4. Create an Action Configuration (mwo_powerpagesactionconfiguration) with an operation name that you will pass from Power Pages and the unique name of the Custom API you want to call. You will find Action Configuration in the Power Pages Management App in the section Actions.
5. Create Webfile actions.js, include the content from actions.js in this repository and then include it on your page html `<script src="/actions.js"></script>`.
6. Call `callAction(operation, inputs, onSuccess, onError)` where operation is the operation name you assigned to the Action Configuration and inputs is an object with the Request Parameters of the Custom API. onSuccess and onError are functions receiving the `mwo_outputs` as a parsed object.
    - Success Responses contain the Response Properties of the Custom API in the object.
    - Error Responses from the Custom API/Adapter will have a property message.
    - Error Responses from the Power Page OData API will be passed raw (unless it's a known configuration error) and are likely to have an error object with a message property.
    - It is advised to check `var errorMessage = outputs.message || outputs.error?.message;` for most common errors in the onError function.

## Security 
TODO

## Sample Code
The sample code calls a Custom API (adc_ValidateVAT) with a Request Parameter VAT and three Response Properties Valid, Name and Address. It is assumed that the Action Configuration has set "ValidateVAT" as the Operation.
In this sample, a missing Site Setting, Table Permission or Action Configuration will be reported to the html element with the id "valid".

```
$(() => {
    $("#vat").change(() => {
        var inputs = {};
        if ($("#vat").val())
            inputs.VAT = $("#vat").val();

        actions.callAction("ValidateVAT", inputs, onSuccess, onError)
    });

    function onSuccess(outputs) {
        $("#valid").text(`Valid? ${outputs.Valid}`);
        $("#name").text(`Name: ${outputs.Name}`);
        $("#address").text(`Address: ${outputs.Address}`);
    }

    function onError(outputs) {
        $("#valid").text(`Error: ${outputs.message || outputs.error?.message}`);
        $("#name").text("");
        $("#address").text("");
    }
});
```
