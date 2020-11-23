mergeInto(LibraryManager.library, {
    stringReturnValueFunction: function (message) {
    var string =  Pointer_stringify(message);
    var bufferSize = lengthBytesUTF8(string) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(string, buffer, bufferSize);
    return buffer;
    },
    getCharacter: function (username, password) {
        var request = new XMLHttpRequest()
        request.open('GET', 'https://www.polygon.us/apiEscuelaspp/public/Usuarios/' + Pointer_stringify(username) + '/' + Pointer_stringify(password), true)
        request.onload = function () 
        {
            if (request.status >= 200 && request.status < 400) 
                unityInstance.SendMessage('Receiver', 'Receive', this.response);
            else 
                unityInstance.SendMessage('Receiver', 'Receive', '>_<');
        }
        
        request.send();
    },
    setTime: function (time)
    {
        var request = new XMLHttpRequest()
        request.open('POST', 'https://www.polygon.us/apiEscuelaspp/public/StillAlive', true)
        request.setRequestHeader("Content-Type", "application/json");
        request.onload = function () 
        {
            if (request.status >= 200 && request.status < 400) 
                unityInstance.SendMessage('Receiver', 'Receive', this.response);
            else 
                unityInstance.SendMessage('Receiver', 'Receive', '>_<');
        }
        
        request.send(Pointer_stringify(time));
    }
});