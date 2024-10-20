var dataType;
var dataobj;
var tempFilePath;
var isPdfSigning;
var accessControlHeader = {
    'Access-Control-Allow-Origin': '*',
    'Content-Type': "application/json",
    'X-Requested-With': 'XMLHttpRequest',
    'Cache-Control': 'no-cache'
};

$(document).ready(function () {
    debugger;
    callListCHProvider();
    $("#ddlTokens").on("change", callListCertificate);
    
    $("#CBPdfSigning").on("change", changeContainer)
});

function changeContainer() {
    debugger;
    isPdfSigning = $("#CBPdfSigning");
    //var isPdfSigning= $("#CBPdfSigning").val().toLowerCase() === "on" ? true : false;
    if (isPdfSigning[0].checked) {
        $("#pdfSigningContainer").css("display", "block");
        $("#dataSigningContainer").css("display", "none");
    }
    else {
        $("#pdfSigningContainer").css("display", "none");
        $("#dataSigningContainer").css("display", "block");
    }
}

function callListCHProvider() {
    debugger;
    $("#ddlCert").html("");
    var option = $('<option/>').attr({ 'value': '' });
    option.html("Select Certificate");
    $("#ddlCert").append(option);
    var datatosend = "";
    var listTokenRequest = {
        AppID: Math.random().toString().slice(2, 12),
        tokenStatus: "CONNECTED",
        tokenType: "ALL"
    };
    datatosend = JSON.stringify(listTokenRequest);
    encRequest(datatosend, "ListToken");
}

function callListCertificate() {
    debugger;
    var datatosend = "";
    if ($('#ddlTokens').val().trim() === "") { }
    else {
        var listCertRequest = {
            appID: Math.random().toString().slice(2, 12),
            keyStoreDisplayName: $('#ddlTokens').val(),
            certFilter: {
                commonName: "",
                issuerName: "",
                serialNumber: "",
                isNotExpired: ""
            }
        };
        datatosend = JSON.stringify(listCertRequest);
        encRequest(datatosend, "ListCertificate");
    }
    
}

function callSign() {
    debugger;
    isPdfSigning = $("#CBPdfSigning");
    if (isPdfSigning[0].checked) {
        callPKCSBulkSign();
    }
    else {
        callPKCSSign();
    }
}

function callPKCSSign() {
    debugger;
    var datatosend = "";
    var signRequest = {
        appID: Math.random().toString().slice(2, 12),
        keyStorePassphrase: $('#tokenPassword').val().trim(),
        keyStoreDisplayName: $('#ddlTokens').val().trim(),
        keyId: $('#ddlCert').val().trim(),
        dataToSign: $('#dataToSign').val().trim(),
        dataType: $('#ddlDataType').val().trim(),
        timeStamp: Date.now()
    };
    datatosend = JSON.stringify(signRequest);
    encRequest(datatosend, "PKCSSign");

}

function callPKCSBulkSign() {
    debugger;
    var datatosend = "";
    var signRequest = {
        appID: Math.random().toString().slice(2, 12),
        keyStorePassphrase: $('#tokenPassword').val().trim(),
        keyStoreDisplayName: $('#ddlTokens').val().trim(),
        keyId: $('#ddlCert').val().trim(),
        PDFName: $("#hfPdfFileName").val(),
        PdfFileBase64: $("#hfPdfFileBase64").val()
    };
    datatosend = JSON.stringify(signRequest);
    encRequest(datatosend, "PKCSBulkSign");
    //encRequest(datatosend, "PKCSSingleSign");

}

function encRequest(datatosend, type) {
    debugger;
    //alert(JSON.stringify({ 'Type': type, 'Data': datatosend }));
    var ajaxReq = {
        url: '/NewDSC/NewDSC/Encrypt',
        type: "POST",
        //data: { jsonData: JSON.stringify({ 'Type': type, 'Data': datatosend }) },
        data: { Data: datatosend, Type: type, jsonData: JSON.stringify({ 'Type': type, 'Data': datatosend })},
        success: function (data, keyID) {
            dataType = type;
            dataobj = JSON.parse(data);
            tempFilePath = dataobj.tempFilePath;
            console.log(data);
            CallService(type, data);

        },
        error: APICallError

    };
    $.ajax(ajaxReq);
}

function CallService(endPoint, data) {
    debugger;
    $("#divLoading").addClass('show');
    //if (endPoint == 'PKCSSingleSign') {
    //    endPoint = 'PKCSBulkSign';
    //}
    var ajaxReq = {
        
        url: 'https://localhost.emudhra.com:26769/DSC/' + endPoint,
        //url: 'https://localhost.emudhra.com:26769/DSC/' ,
        crossDomain: true,
        type: "POST",
        headers: accessControlHeader,
        data: data,
        success: serviceCallSuccess,
        error: APICallError
    };
    $.ajax(ajaxReq);
}

function serviceCallSuccess(data) {
    debugger;
    if (data.status === 1) {
        decRequest(data.responseData);
    }
}

function decryptCallComplete() {
    debugger;
    $("#divLoading").removeClass('show');
}

function APICallError(jqXHR, textStatus, errorThrown) {
    debugger;
    if (jqXHR.status === 0) {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('<b>Please check any one of the cause</b></br><ul><li>emBridge service is not running</li><li>emBridge is not installed</li></ul>');
    } else if (jqXHR.status === 404) {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Requested endpoint not found. [404]');

    } else if (jqXHR.status === 500) {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Internal Server Error [500].');
    } else if (exception === 'parsererror') {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Requested JSON parse failed.');
    } else if (exception === 'timeout') {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Time out error.');
    } else if (exception === 'abort') {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Ajax request aborted.');
    } else {
        $("#btnErrorPopup").click();
        $("#errorMessage").html('Uncaught Error.\n' + jqXHR.responseText);
    }
}

function decRequest(encryptedJson) {
    debugger;
    var datatosend = encryptedJson;
    //if (tempFilePath != null)
    //    while (/\\/.test(tempFilePath))
    //        tempFilePath = tempFilePath.replace('\\', '/');
    var ajaxReq = {
        url: '/NewDSC/NewDSC/Decrypt',
        type: "POST",
        success: decryptCallSuccess,
        complete: decryptCallComplete,
        error: APICallError,
        //data: JSON.stringify({ 'Type': dataType, 'Data': encryptedJson, 'TempFilePath': tempFilePath })
        data: { Type: dataType, Data: encryptedJson, TempFilePath: tempFilePath, jsonData: JSON.stringify({ 'Type': dataType, 'Data': encryptedJson, 'TempFilePath': tempFilePath }) }
    };
    $.ajax(ajaxReq);
}

function decryptCallSuccess(data) {
    debugger;
    obj = JSON.parse(data);
    //alert(obj);
    switch (dataType) {
        case "ListToken":
            var tokenData = obj.tokens;
            generateDropDown(tokenData, "ddlTokens");
            break;
        case "ListCertificate":
            var certData = obj.Certificates;
            generateCertDropDown(certData, "ddlCert");
            break;
        case "PKCSSign":
            $("#signedData").html(obj.SignedText);
            //Commented to hide Signed data(Sunil Deshalahre 06-07-22)
            //$("#sdContainer").css("display", "block");
            //$("#sdPdfContainer").css("display", "none");
            $("#signedData").addClass("divSignedData");
            DataSignCompleted(obj.SiginedCertificate, obj.SignedText, obj.Status);
            break;
        case "PKCSBulkSign":
            $("#sdContainer").css("display", "none");
            $("#sdPdfContainer").css("display", "block");
            var items = obj.bulkSignItems;
            //alert($("#signedData").html());
            //$("#signedDataPDF").html(items[0].signedData);
            if (items[0].signedData.length > 0) {
                debugger;
                SignCompleted(items[0].signedData, items[0].status);
            }
            //for (i = 0; i < items.length; i++) {
            //    var textArea = $('<textarea />').attr({ 'class': 'form-control divSignedData', 'id': 'signedData' + i });
            //    textArea.html(items[i].signedData);
            //    var textCopy = $('<button />').attr({ 'class': "form-control btn-dark", 'onclick': "copyFunction('#signedData" + i + "');" });
            //    textCopy.html("Copy Signed Data")
            //    var div = $('<div />');
            //    div.append(textArea);
            //    div.append(textCopy);
            //    $("#sdPdfContainer").append(div);
            //}
            break;
        //case "PKCSBulkSign":
        //    $("#sdContainer").css("display", "none");
        //    $("#sdPdfContainer").css("display", "block");
        //    var items = obj.bulkSignItems;
        //    for (i = 0; i < items.length; i++) {
        //        var textArea = $('<textarea />').attr({ 'class': 'form-control divSignedData', 'id': 'signedData' + i });
        //        textArea.html(items[i].signedData);
        //        var textCopy = $('<button />').attr({ 'class': "form-control btn-dark", 'onclick': "copyFunction('#signedData" + i +"');" });
        //        textCopy.html("Copy Signed Data")
        //        var div = $('<div />');
        //        div.append(textArea);
        //        div.append(textCopy);
        //        $("#sdPdfContainer").append(div);
        //    }
        //    break;
    }
}

function generateDropDown(objArray, dDMode) {
    debugger;
    var array = [];
    var object;
    var id = "";
    array = objArray;
    i = 0;
    id = "#" + dDMode;
    $(id).html("");
    var option = $('<option/>').attr({ 'value': '' });
    option.html("Select Device");
    $(id).append(option);
    while (i < array.length) {
        object = array[i];
        var option = $('<option/>').attr({ 'value': object.keyStoreDisplayName });
        option.html(object.keyStoreDisplayName);
        if (object.keyStoreDisplayName === 'Save as PFX/P12') { }
        else { $(id).append(option); }
        i++;
    }
    $(id).focus();
}

function generateCertDropDown(objArray, ddCertMode) {
    debugger;
    var array = [];
    var object;
    var id = "";
    array = objArray;
    i = 0;
    id = "#" + ddCertMode;
    $(id).html("");
    var option = $('<option/>').attr({ 'value': '' });
    option.html("Select Certificate");
    $(id).append(option);
    while (i < array.length) {
        object = array[i];
        var option = $('<option/>').attr({ 'value': object.KeyId });
        option.html(object.CommonName);
        $(id).append(option);
        i++;
    }
}

function copyFunction(id) {
    debugger;
    var $temp = $("<input>");
    $("body").append($temp);
    var x = $(id).html();
    $temp.val(x).select();
    document.execCommand("copy");
    $temp.remove();
}

