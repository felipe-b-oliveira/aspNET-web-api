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

    $('#myModal').modal('hide')
}

function novoAluno() {
    var btnSave = document.querySelector('#btnSave');
    var modalTitle = document.querySelector('#modalTitle');

    document.querySelector('#Name').value = '';
    document.querySelector('#LastName').value = '';
    document.querySelector('#Phone').value = '';
    document.querySelector('#Registry').value = '';

    _student = {};

    btnSave.textContent = "Cadastrar";

    modalTitle.textContent = "Cadastrar Aluno";

    $('#myModal').modal('show')
}

function Cancel() {
    var btnSave = document.querySelector('#btnSave');
    var modalTitle = document.querySelector('#modalTitle');

    document.querySelector('#Name').value = '';
    document.querySelector('#LastName').value = '';
    document.querySelector('#Phone').value = '';
    document.querySelector('#Registry').value = '';

    _student = {};

    btnSave.textContent = "Cadastrar";

    modalTitle.textContent = "Cadastrar Aluno";

    $('#myModal').modal('hide')
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

function erase(student) {

    bootbox.confirm({
        message: `Tem certeza de que deseja excluir ${student.Name}?`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                deleteStudent(student.Id);
                readStudents();
            }
        }
    });

}

readStudents();

function editStudent(student) {
    var btnSave = document.querySelector('#btnSave');
    var modalTitle = document.querySelector('#modalTitle');
    document.querySelector('#Name').value = student.Name;
    document.querySelector('#LastName').value = student.LastName;
    document.querySelector('#Phone').value = student.Phone;
    document.querySelector('#Registry').value = student.Registry;

    btnSave.textContent = "Salvar";

    modalTitle.textContent = `Editar Aluno(a) ${student.Name}`;

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
                    <td>
                        <button a href="javascript:void(0);" data-toggle="modal" data-target="#myModal" class="btn btn-info" onclick='editStudent(${JSON.stringify(student)})'>Editar</button>
                        <button a href="javascript:void(0);" class="btn btn-danger" onclick='erase(${JSON.stringify(student)})'>Deletar</button>
                    </td>
                </tr>
                `
    tbody.innerHTML += trow;
}