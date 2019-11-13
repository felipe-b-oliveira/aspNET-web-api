var tbody = document.querySelector('table tbody');
var student = {};

function Register() {
    _student.name = document.querySelector('#Name').value;
    _student.lastName = document.querySelector('#LastName').value;
    _student.phone = document.querySelector('#Phone').value;
    _student.registry = document.querySelector('#Registry').value;

    console.log(_student);

    if (_student.id === undefined || _student.id === 0) {
        saveStudents('POST', 0, _student);
    }
    else {
        saveStudents('PUT', _student.id, _student);
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
    xhr.open(`GET`, `https://localhost:44390/api/Student/Recover `, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));


    xhr.onerror = function () {
        console.error('ERRO', xhr.readyState);
    }

    // Creating anonymous function
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var students = JSON.parse(this.responseText);
                for (var index in students) {
                    createLine(students[index]);
                }
            }
            else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
                console.log(erro.Message);
                console.log(erro.ExceptionMessage);
            }
        }

    }

    // AJAX call
    xhr.send();
}

function saveStudents(method, Id, body) {
    // Creating XML Http Request
    var xhr = new XMLHttpRequest();

    if (Id === undefined || Id === 0)
        id = '';

    /* Method open, assyncronous call
    xhr.addEventListener("load", function () {
        initialArray = JSON.parse(xhr.response);
    }, false);
    xhr.open(method, `https://localhost:44390/api/Student/${Id} `); */

    // Method open, syncronous call
    xhr.open(method, `https://localhost:44390/api/Student/${Id} `, false);

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
        message: `Tem certeza de que deseja excluir ${student.name}?`,
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
                deleteStudent(student.id);
                readStudents();
            }
        }
    });

}

readStudents();

function editStudent(student) {
    var btnSave = document.querySelector('#btnSave');
    var modalTitle = document.querySelector('#modalTitle');
    document.querySelector('#Name').value = student.name;
    document.querySelector('#LastName').value = student.lastName;
    document.querySelector('#Phone').value = student.phone;
    document.querySelector('#Registry').value = student.registry;

    btnSave.textContent = "Salvar";

    modalTitle.textContent = `Editar Aluno(a) ${student.name}`;

    _student = student;

    console.log(_student);
}

function createLine(student) {

    //var registry = student.Registry;

    var trow = `<tr>
                    <td>${student.name}</td>
                    <td>${student.lastName}</td>
                    <td>${student.phone}</td>
                    <td>${student.registry}</td>
                    <td>
                        <button a href="javascript:void(0);" data-toggle="modal" data-target="#myModal" class="btn btn-info" onclick='editStudent(${JSON.stringify(student)})'>Editar</button>
                        <button a href="javascript:void(0);" class="btn btn-danger" onclick='erase(${JSON.stringify(student)})'>Deletar</button>
                    </td>
                </tr>
                `
    tbody.innerHTML += trow;
}
