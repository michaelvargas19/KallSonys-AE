const setEditModal = (isbn) => {
    const xhttp = new XMLHttpRequest();

       
    xhttp.open("GET", `http://localhost:3000/book/${isbn}`, false);
    xhttp.send();

    const book = JSON.parse(xhttp.responseText);

    const {
        title, 
        author, 
        publisher, 
        publish_date,
        numOfPages
    } = book;

    document.getElementById('isbn').value = isbn;
    document.getElementById('title').value = title;
    document.getElementById('author').value = author;
    document.getElementById('publisher').value = publisher;
    document.getElementById('publish_date').value = publish_date;
    document.getElementById('numOfPages').value = numOfPages;

    // setting up the action url for the book
    document.getElementById('editForm').action = `http://localhost:3000/book/${isbn}`;
}

const deleteBook = (isbn) => {
    const xhttp = new XMLHttpRequest();

    console.log("Eliminar");
    xhttp.open("DELETE", `http://localhost:3000/book/${isbn}`, false);
    xhttp.send();

    location.reload();
}

const loadProducts = () => {
    const xhttp = new XMLHttpRequest();

    //xhttp.open("PUT", "http://localhost:8081/WsPortalUsuariosRest-web/ws/WsPortalUsuariosRest/autentica/", false);
    //xhttp.send()

    xhttp.open("GET", "http://localhost:3000/products-list", false);
    xhttp.send();

    const products = JSON.parse(xhttp.responseText);

    // console.log(products);

    for (let product of products) {
        const x = `
                <div class="col-md-4 product simpleCart_shelfItem text-center">
                    <a href="single.html"><img class="item_image" src="/images/p${product.id}.jpg" alt="" /></a>
                    <div class="mask">
                        <a href="/products/${product.sku}">Quick View</a>
                    </div>
                    <a class="product_name item_name" href="single.html">${product.name}</a>
                    <p class="item_description">${product.description}</p>
                    <br/>
                    <p><a class="item_add" href="javascript:;"><i></i> <span class="item_price">$${product.price}</span></a></p>
                </div>
        `

        document.getElementById('products').innerHTML = document.getElementById('products').innerHTML + x;
    }
}



loadProducts();