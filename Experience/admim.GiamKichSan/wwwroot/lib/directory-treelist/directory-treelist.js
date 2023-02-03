let directoryTreelist = {
    init: function () {
        var toggler = document.getElementsByClassName("directory-caret");
        for (var indexItem = 0; indexItem < toggler.length; indexItem++) {
            if (toggler[indexItem].getAttribute('cust-event') != 'true') {
                toggler[indexItem].setAttribute('cust-event', 'true');
                toggler[indexItem].addEventListener("click", function () {
                    this.parentElement.querySelector(".nested").classList.toggle("active");
                    this.classList.toggle("caret-down");
                });
            }
        }
    }
};
directoryTreelist.init();
