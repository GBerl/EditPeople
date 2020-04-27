$(() => {
    showPeople();
    $("#btn-add").on('click', () => {
        $("#modal").modal();
        $("#title").text('New Person');
        $("#edit-save").hide();
        $("#add-save").show();
        $("#close").on('click', () => {
            clear();
        })
        $("#add-save").on('click', () => {
            const person = {
                firstName: $("#fname").val(),
                lastName: $("#lname").val(),
                age: $("#personAge").val()
            }
            clear();
            $.post(`/home/addperson`, person, function (p) {
                showPeople();

            });

        });
    })
})
$('#people-table').on('click', '.edit', function () {
    const button = $(this);
    const tr = button.closest('tr').bind("data-id");
    const cells = button.closest('tr').children();
    $("#modal").modal();
    $("#title").text('Edit Person');
    $("#add-save").hide();
    $("#edit-save").show();

    $("#fname").val(cells.eq(0).text());
    $("#lname").val(cells.eq(1).text());
    $("#personAge").val(cells.eq(2).text());
    $("#id").val(tr.attr("data-id"));
    $("#close").on('click', () => {
        clear();
    })
});
$("#edit-save").on('click', () => {
    var person = {
        id: $("#id").val(),
        firstName: $("#fname").val(),
        lastName: $("#lname").val(),
        age: $("#personAge").val()
    }
    clear();
    $.post(`/home/edit`, person, function (p) {
        showPeople();

    });
});

function clear() {
    $("#fname").val('');
    $("#lname").val('');
    $("#personAge").val('');
}
$('#people-table').on('click', '.delete', function () {
    const button = $(this);
    const tr = button.closest('tr').bind("data-id");
    const id = tr.attr("data-id");
    const cells = tr.children();

    const person = {
        firstName: cells.eq(0).text(),
        lastName: cells.eq(1).text(),
        age: cells.eq(2).text(),
        id
    }
    $.post(`/home/delete`, person, function (p) {
        showPeople();
    });

})
function showPeople() {
    $("#people-table tr:not(:first)").remove();
    $.get(`/home/showpeople`, function (people) {
        people.forEach(p =>
            $("#people-table tbody").append(
                ` <tr data-id="${p.id}">
            <td id="firstName">${p.firstName}</td>
            <td id="lastName">${p.lastName}</td>
            <td id="age">${p.age}</td>
            <td>
        <button class="btn btn-primary edit">Edit</button>
        <button class="btn btn-danger delete">Delete</button>
           </td>
                 </tr>`)
        );
    });
}



