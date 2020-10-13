var PopupOpenerPlugin = {
 var PopupOpenerCaptureClick: function() {
     OpenPopup = function() {
      window.open('http://unity3d.com', null, 'width=500,height=500');
    document.getElementById('canvas').removeEventListener('click', OpenPopup);
    };
   document.getElementById('canvas').addEventListener('click', OpenPopup, false);
  }
};
mergeInto(LibraryManager.library, PopupOpenerPlugin);
