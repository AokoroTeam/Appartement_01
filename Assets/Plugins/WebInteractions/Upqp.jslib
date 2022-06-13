
mergeInto(LibraryManager.library, {
    FocusCanvas : function(focus) {
        if (gameReady) {
            SendMessage("GameManager", "FocusCanvas", focus);
        }
    },
 });