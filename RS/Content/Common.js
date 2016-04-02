var changed = false;
$(function () {
    $(function () {
        $(document).bind('keyup', function (event) {
            if (event.keyCode === 27) {
                disableInputs();
            }
        });

        $('td').on('change', 'input', function (event) {
            changed = true;
        });

        $('.cancel-button').bind('click', function () {
            disableInputs();
            $(this).parent().toggle();
            $(this).parent().parent().find('.EditButton').show();
        });

        $('.EditButton').button().bind('click', function (event) {
            event.preventDefault();
            disableInputs();

            disabled = false;
            
            var tr = event.target.offsetParent.parentElement;
            $(tr).find('td.editable2').find('.submit-wrapper').show();
            $(tr).find("td").not(".editable,.editable2,.dont").each(function (index, item) {
                convertToInput(item);
            });

            var roles = $(tr).find('td.editable');
            activateAllCheckBoxes(roles);

            $(this).hide();
        });
        $('button').button();
    });
});

function disableInputs() {
    if (changed) {
        $('#saveModal').openModal();
    }

    $('td').not('.editable,.editable2,.dont').each(function (index, item) {
        convertToSpan(item);
    });

    deactivateAllCheckBoxes();
}

function convertToInput(td) {
    var input = $('<input type="text">');
    input.val(td.textContent);
    $(td).find('span').replaceWith(input);
}

function convertToSpan(td) {
    var span = $('<span>');
    span.text($(td).find('input').val());
    $(td).find('input').replaceWith(span);
}

function activateAllCheckBoxes(td) {
    $(td).find('input').each(function (index, item) {
        $(item).removeAttr('disabled');
    });
}

function deactivateAllCheckBoxes(td) {
    $('td.editable').find('input').each(function (index, item) {
        $(item).attr('disabled', 'disabled');
    });
}