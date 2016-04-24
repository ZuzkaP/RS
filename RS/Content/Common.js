var changed = false;
$(function () {
    $(function () {
        $(".button-collapse").sideNav();
        $('.parallax').parallax();

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
        });
        $('.save-button').bind('click', function () {
            saveChanges();
            disableInputs();
        });

        $('.EditButton').button().bind('click', function (event) {
            event.preventDefault();
            disableInputs();

            var tr = event.target.offsetParent.parentElement.parentElement;
            $(tr).find('td.editable2').find('.submit-wrapper').show();
            $(tr).find("td").not(".editable,.editable2,.dont").each(function (index, item) {
                convertToInput(item);
            });

            $(tr).addClass('active-row');
            var roles = $(tr).find('td.editable');
            activateAllCheckBoxes(roles);

            $(this).hide();
        });

        $('.EditButton').button();
        $('.save-button').button();
        $('.cancel-button').button();

    });
});

function disableInputs() {
    if (changed) {
        $('#saveModal').openModal();
        changed = false;
    }

    $('td').not('.editable,.editable2,.dont').each(function (index, item) {
        convertToSpan(item);
    });

    $('td.editable2').find('.submit-wrapper').each(function (index, item) {
        if ($(item).is(':visible')) {
            $(item).hide();
            $(item).parent().find('.EditButton').show();
        }
    });

    deactivateAllCheckBoxes();
    //najdem class active row a zmazem
    $('tr.active-row').removeClass('active-row');
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

function saveChanges()
{
    var tr = $('tr.active-row');
    var inputs = tr.find('input[type=text]');
    var roles = tr.find('input[type=checkbox]');
    var email = tr.find('.dont').find('span').text();

    var name = $(inputs[0]).val();
    var last_name = $(inputs[1]).val();
    var number = $(inputs[2]).val();

    var admin = {
        name: 'admin',
        active: $(roles[0]).is(':checked')
    };
    var uzivatel ={
        name: 'uzivatel',
        active: $(roles[1]).is(':checked')
    };
    var trener = {
        name: 'trener',
        active:  $(roles[2]).is(':checked')
    };
    var array = [admin,uzivatel,trener];

    $.ajax({
        type: "POST",
        url: "/User/Save",
        data: {
            'first_name': name,
            'last_name': last_name,
            'number': number,
            'email': email,
            'Roles': getRoles(array)
        },
        dataType: 'json'
    })
}

function getRoles(roles){

    var datas = [];
    $.each(roles,function(index,item){
        if(item.active)
        {
            datas.push({"name": item.name});
        }
    
    })

}