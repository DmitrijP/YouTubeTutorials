


function CreateSortButtons(tableId) {
    var table = document.getElementById(tableId);
    var thead = table.getElementsByTagName('thead')[0];
    var row = thead.rows[0];
    var cells = row.cells;
    for (var i = 0; i < cells.length; i++) {
        if (cells[i].getAttribute('data-sort') == 'true') {
            cells[i].appendChild(CreateButton());
        }
    }
}

function CreateButton() {
    var button = document.createElement('button');
    button.innerHTML = '&#8593;&#8595;';
    button.classList.add('btn');
    button.classList.add('btn-sm');
    button.classList.add('btn-outline-secondary');
    button.addEventListener('click', ExecuteTableSort);
    return button;
}

function ExecuteTableSort() {
    var btn = this;
    var th = btn.parentElement;
    var tr = th.parentElement;
    var thead = tr.parentElement;
    var table = thead.parentElement;
    var body = table.getElementsByTagName('tbody')[0];
    var rows = body.rows;
    var rowsArray = [].slice.call(rows);

    var index = th.cellIndex;
    var sortType = th.getAttribute('data-sortType');
    var sortDir = th.getAttribute('data-sortDirection');
    var sortedRows = rowsArray.sort((a, b) => sortRows(a, b, index, sortType, sortDir));
    body.innerHTML = '';
    for (var i = 0; i < sortedRows.length; i++) {
        body.appendChild(sortedRows[i]);
    }
    if (sortDir === 'desc') {
        th.setAttribute('data-sortDirection', 'asc');
    } else {
        th.setAttribute('data-sortDirection', 'desc');
    }
}

function sortRows(a, b, index, sortType, sortDir) {
    if (sortType === 'int') {
        if (sortDir === 'desc') {
            return sortIntRowsDesc(a, b, index);
        }
        return sortIntRows(a, b, index);
    }
    if (sortType === 'custom') {
        if (sortDir === 'desc') {
            return sortByCustomKeyDesc(a, b, index);
        }
        return sortByCustomKey(a, b, index);
    }
    if (sortDir === 'desc') {
        return sortStringRowsDesc(a, b, index);
    }
    return sortStringRows(a, b, index);
}

function sortStringRows(a,b, index) {
    var A = a.cells[index].innerHTML.toUpperCase();
    var B = b.cells[index].innerHTML.toUpperCase();
    if (A < B)
        return -1;
    if (A > B)
        return 1;
    return 0;
}

function sortStringRowsDesc(a, b, index) {
    var A = a.cells[index].innerHTML.toUpperCase();
    var B = b.cells[index].innerHTML.toUpperCase();
    if (A > B)
        return -1;
    if (A < B)
        return 1;
    return 0;
}

function sortIntRows(a, b, index) {
    var A = a.cells[index].innerHTML;
    var B = b.cells[index].innerHTML;
    return A - B;
}

function sortIntRowsDesc(a, b, index) {
    var A = a.cells[index].innerHTML;
    var B = b.cells[index].innerHTML;
    return B - A;
}

function sortByCustomKey(a,b,index) {
    var A = a.cells[index].getAttribute('data-sortKey').toUpperCase();
    var B = b.cells[index].getAttribute('data-sortKey').toUpperCase();
    if (A < B)
        return -1;
    if (A > B)
        return 1;
    return 0;
}

function sortByCustomKeyDesc(a, b, index) {
    var A = a.cells[index].getAttribute('data-sortKey').toUpperCase();
    var B = b.cells[index].getAttribute('data-sortKey').toUpperCase();
    if (A > B)
        return -1;
    if (A < B)
        return 1;
    return 0;
}

CreateSortButtons('employeeTable');

CreateSortButtons('employeeSearchTable');