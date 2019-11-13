var tbody = document.querySelector('table tbody');
var _student = {};

function Register() {
    _student.Name = document.querySelector('#Name').value;
    _student.LastName = document.querySelector('#LastName').value;
    _student.Phone = document.querySelector('#Phone').value;
    _student.Registry = document.querySelector('#Registry').value;

    console.log(_student);

    if (_student.Id === undefined || _student.Id === 0) {
        saveStudents('POST', 0, _student);
    }
    else {
        saveStudents('PUT', _student.Id, _student);
    }

    readStudents();
}

function Cancel() {
    var btnSave = document.querySelector('#btnSave');
    var btnCancel = document.querySelector('#btnCancel');
    var title = document.querySelector('#title');

    document.querySelector('#Name').value = '';
    document.querySelector('#LastName').value = '';
    document.querySelector('#Phone').value = '';
    document.querySelector('#Registry').value = '';

    _student = {};

    btnSave.textContent = "Register";
    btnCancel.textContent = "Clear";

    title.textContent = "Register Student";
}

function readStudents() {

    tbody.innerHTML = '';

    // Creating XML Http Request
    var xhr = new XMLHttpRequest();

    // Method open, assyncronous call
    xhr.open(`GET`, `https://localhost:44390/api/Student/`, true);

    // Creating anonymous function
    xhr.onload = function () {
        var students = JSON.parse(this.responseText);
        for (var index in students) {
            createLine(students[index]);
        }
    }

    // AJAX call
    xhr.send();
}

function saveStudents(method, Id, body) {
    // Creating XML Http Request
    var xhr = new XMLHttpRequest();

    if (Id === undefined || Id === 0)
        Id = '';

    /* Method open, assyncronous call
    xhr.addEventListener("load", function () {
        initialArray = JSON.parse(xhr.response);
    }, false);
    xhr.open(method, `https://localhost:44390/api/Student/${Id}`);*/

    // Method open, syncronous call
    xhr.open(method, `https://localhost:44390/api/Student/${Id}`, false);

    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(body));
}

function deleteStudent(Id) {
    // Creating XML Http Request
    var xhr = new XMLHttpRequest();

    // Method open, syncronous call
    xhr.open(`DELETE`, `https://localhost:44390/api/Student/${Id}`, false);

    xhr.send();
}

function erase(Id) {
    deleteStudent(Id);
    readStudents();
}

readStudents();

function editStudent(student) {
    var btnSave = document.querySelector('#btnSave');
    var btnCancel = document.querySelector('#btnCancel');
    var title = document.querySelector('#title');
    document.querySelector('#Name').value = student.Name;
    document.querySelector('#LastName').value = student.LastName;
    document.querySelector('#Phone').value = student.Phone;
    document.querySelector('#Registry').value = student.Registry;

    btnSave.textContent = "Save";
    btnCancel.textContent = "Cancel";

    title.textContent = `Edit Student ${student.Nome}`;

    _student = student;

    console.log(_student);
}

function createLine(student) {

    //var registry = student.Registry;

    var trow = `<tr>
                                <td>${student.Name}</td>
                                <td>${student.LastName}</td>
                                <td>${student.Phone}</td>
                                <td>${student.Registry}</td>
                                <td><button onclick='editStudent(${JSON.stringify(student)})'>Edit</button></td>
                                <td><button onclick='erase(${student.Id})'>Delete</button></td>
                            </tr>
                            `
    tbody.innerHTML += trow;
}