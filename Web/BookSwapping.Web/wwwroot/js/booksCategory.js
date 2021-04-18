const main = document.getElementById('main');
const historyBtn = document.getElementById('historyBtn');
const techLiteratureBtn = document.getElementById('techLiteratureBtn');
const showAllBtn = document.getElementById('showAllBtn');
let showCategories = [];

// Get category
function getCategory(value) {

    let xhr = new XMLHttpRequest();

    xhr.onload = function () {

        var data = JSON.parse(this.responseText);
        const category = [];

        if (value != 'Покажи всички') {
            data = data.filter(genre => genre.genre == value);
        }

        for (var i = 0; i < data.length; i++) {

            category.push(data[i]);
        }

        showCategories = category;

        updateCategory(showCategories);
    };

    xhr.open("GET", 'https://localhost:44331/library/allbookinlibrarydata');
    xhr.send();
}

// Update page with chosen category
function updateCategory(item) {
    main.innerHTML = '';

    for (var i = 0; i < item.length; i++) {
        let imageFormat = 'data:image/png;base64,' + item[i].imageContent;
        var div =
            `<div class="card-header m-2">
                <div class="row">
                    <div class="col-auto">
                        <img src=${imageFormat} class="img-fluid img-thumbnail" alt="${item[i].bookName}" style="width:150px;"/>
                    </div>
                    <div class="col">
                        <div class="card-body">
                            <h5 class="card-title">
                                <a class="text-danger text-decoration-none" href="/book/bookdetails/${item[i].bookId}">
                                    ${item[i].bookName}
                                </a>
                            </h5>
                            <h6 class="card-subtitle mt-2">
                                Автор:
                                <a class="text-primary text-decoration-none" href="/author/${item[i].authorName}?id=${item[i].authorId}">
                                    ${item[i].authorName}
                                </a>
                            </h6>
                            <h6 class="card-subtitle mt-2">
                                Жанр:
                                <a class="text-primary text-decoration-none" id="genre" href="/genre/${item[i].genre}">
                                    ${item[i].genre}
                                </a>
                            </h6>
                            <p class="card-subtitle mt-2">Публикувана на: ${item[i].dateTime}</p>
                            <p class="card-subtitle mt-2">Публикувана от: ${item[i].uploadBy}</p>
                        </div>
                    </div>
                </div>
            </div>`
        main.innerHTML += div;
    }
}

// Event listener
historyBtn.addEventListener('click', function () {
    getCategory(historyBtn.value);
});
techLiteratureBtn.addEventListener('click', function () {
    getCategory(techLiteratureBtn.value);
});
showAllBtn.addEventListener('click', function () {
    getCategory(showAllBtn.value);
});


