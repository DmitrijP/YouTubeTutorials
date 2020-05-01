
function BindEvents() {
    var table = document.getElementById('employeeTable');
    var inputs = table.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener('keyup', QueryTableColumn);
    }
}

function QueryTableColumn() {
    var inputElement = this;
    var index = inputElement.getAttribute('data-index');
    var searchString = inputElement.value;
    var table = document.getElementById('employeeTable');
    var bodies = table.getElementsByTagName('tbody');
    var body = bodies[0];
    var rows = body.rows;
    for (var i = 0; i < rows.length; i++) {
        var tdChild = rows[i].children[index];
        if (!tdChild.textContent.includes(searchString)) {
            rows[i].style.display = 'none';
        } else {
            rows[i].style.display = '';
        }
    }

}

function ShowDataInTable(data) {
    var table = document.getElementById('employeeSearchTable');
    var tbody = table.getElementsByTagName('tbody')[0];
    tbody.innerHTML = '';

    for (var i = 0; i < data.length; i++) {
        var row = tbody.insertRow(0);

        var cell8 = row.insertCell(0);
        cell8.innerHTML = data[i].companyName;
        row.insertCell(0).innerHTML = data[i].position;
        row.insertCell(0).innerHTML = data[i].city;
        row.insertCell(0).innerHTML = data[i].birthday;
        row.insertCell(0).innerHTML = data[i].lastName;
        row.insertCell(0).innerHTML = data[i].firstName;
        row.insertCell(0).innerHTML = data[i].userName;
        row.insertCell(0).innerHTML = data[i].id;
    }
}

function ServerSideQuery() {
    var searchString = document.getElementById('serverSearchField').value;
    var url = '/Home/Find?searchString=' + searchString;
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.onload = function () {
        if (xhr.status === 200) {
            var data = JSON.parse(xhr.responseText);
            ShowDataInTable(data);
        } else {
            alert(xhr.status, xhr.responseText);
        }
    };
    xhr.send();
}

function HandleDeleteEmployeeResult(data) {
    var table = document.getElementById('employeeTable');
    var body = table.getElementsByTagName('tbody')[0];
    var rows = body.rows;
    for (var i = 0; i < rows.length; i++) {
        var tdChild = rows[i].children[0];
        if (tdChild.textContent == data.employeeId) {
            body.removeChild(rows[i]);
        }
    }
}

function DeleteEmployee(employeeId) {
    var url = '/Home/Delete?idToDelete=' + employeeId;
    var xhr = new XMLHttpRequest();
    xhr.open('DELETE', url);
    xhr.onload = function () {
        if (xhr.status === 200) {
            var data = JSON.parse(xhr.responseText);
            HandleDeleteEmployeeResult(data);
        } else {
            alert(xhr.status, xhr.responseText);
        }
    };
    xhr.send();
}
function HandleCreateEmployeeResult(data) {
    var table = document.getElementById('employeeTable');
    var tbody = table.getElementsByTagName('tbody')[0];
    var row = tbody.insertRow(0);
                        
    row.insertCell(0).innerHTML =
        '<button class="btn btn-outline-danger" onclick="DeleteEmployee(' + data.id + ')">Delete</button>';
    row.insertCell(0).innerHTML = data.companyName;
    row.insertCell(0).innerHTML = data.position;
    row.insertCell(0).innerHTML = data.city;
    row.insertCell(0).innerHTML = data.birthday;
    row.insertCell(0).innerHTML = data.lastName;
    row.insertCell(0).innerHTML = data.firstName;
    row.insertCell(0).innerHTML = data.userName;
    row.insertCell(0).innerHTML = data.id;
    $('#createEmployeeModal').modal('toggle');

}

function CreateEmployee() {
    var firstName = document.getElementById('firstName').value;
    var lastName = document.getElementById('lastName').value;
    var city = document.getElementById('city').value;
    var position = document.getElementById('position').value;
    var companyName = document.getElementById('copanyName').value;
    var url = '/Home/Create';
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
        if (xhr.status === 200) {
            HandleCreateEmployeeResult(JSON.parse(xhr.responseText));
        } else {
            alert(xhr.status, xhr.responseText);
        }
    };
    xhr.send(JSON.stringify({
        FirstName: firstName,
        LastName: lastName,
        CompanyName: companyName,
        City: city,
        Position: position
    }));
}

document.getElementById('serverSearchField').addEventListener('keyup', ServerSideQuery);
BindEvents();
