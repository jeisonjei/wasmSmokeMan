window.funcs = {
    pop: function () {
        $('.pop').popover({trigger: 'hover', placement: 'auto'})
    },
    selectpicker: function () {
        $(".selectpicker").selectpicker()
    },
    tooltipGeneral: function () {
        $('[data-toggle="tooltip"]').tooltip({trigger: 'hover'})
    },
    tooltipNetwork: function () {
        $('#chipsnetwork').tooltip({trigger: 'hover',offset:'0 25 0 0',html:true})
    },
    scrollResultDown: function () {
        let elem = document.getElementById('results');
        $(elem).scrollTop(3000);
        console.log("hello");
    },
}