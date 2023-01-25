$(document).ready(function () {
    // use the jquery UI datepicker:
    $('input[type=datetime]').datepicker({
        dateFormat: 'yy/mm/dd',
        changeMonth: true,
        changeYear: true,
        yearRange: 'today:+3'
    });

});

$(document).ready(function () {
    $(".lastActionMessageUndo").show(function () {
        $(".lastActionMessageUndo").fadeOut(6000);
    });
});

$(document).ready(function () {
    $(".lastActionMessage").show(function () {
        $(".lastActionMessage").fadeOut(6000);
    });
});

$(document).ready(function () {
    $('#invoices').on('click', 'tbody tr', function () {
        $(this).addClass('highlight').siblings().removeClass('highlight');
    });
});

$(document).ready(function () {
    $('nav').on('click', 'ul li', function () {
        $(this).addClass('active').removeClass('active');
    });
});
