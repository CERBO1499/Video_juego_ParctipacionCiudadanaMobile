mergeInto(LibraryManager.library, {
    getCooking: function()
    {    
       alert("No Function");
    },
    StringReturnValueFunction: function () {
    var string = "Test";
    var bufferSize = lengthBytesUTF8(string) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(string, buffer, bufferSize);
    return buffer;
    },
});