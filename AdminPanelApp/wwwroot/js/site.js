
export function setTheme(themeName) {    
    //add a new css link
    let newLink = document.createElement("link");
    newLink.setAttribute("id", "theme");
    newLink.setAttribute("rel", "stylesheet");
    newLink.setAttribute("type", "text/css");
    newLink.setAttribute("href", `css/app-${themeName}.css`);
    //remove the theme from the head tag
    let head = document.getElementsByTagName("head")[0];
    head.querySelector("#theme").remove();
    //adding newLink in the head
    head.appendChild(newLink);
}

export function downloadFile(mimeType, base64String, fileName) {

    var fileDataUrl = "data:" + mimeType + ";base64," + base64String;
    fetch(fileDataUrl)
        .then(response => response.blob())
        .then(blob => {

            //create a link
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;

            //add, click and remove
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });  
}


export function FileOpenNewTab(mimeType, base64String, fileName) {


    var blob = _base64ToArrayBuffer(mimeType, base64String, fileName);
    var bloburl = URL.createObjectURL(blob);
    window.open(bloburl);
}
function _base64ToArrayBuffer(mimeType, base64String, fileName) {

    var sliceSize = 512;
    var byteCharactor = atob(base64String);
    var byteArrays = [];

    for (var ofset = 0; ofset < byteCharactor.length; ofset += sliceSize) {
        var slice = byteCharactor.slice(ofset,ofset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }


    var blob = new Blob(byteArrays, { type: mimeType, fileName: fileName });

    return blob;
}
export function setScroll() {
    //let's fix scroll here
    var divMessageContainerBase = document.getElementById('divMessageContainerBase');
    if (divMessageContainerBase != null)
        divMessageContainerBase.scrollTop = divMessageContainerBase.scrollHeight;
}