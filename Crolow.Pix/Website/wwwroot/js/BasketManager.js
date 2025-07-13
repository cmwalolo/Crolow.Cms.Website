//var script = document.createElement('script');
//script.src = 'https://code.jquery.com/jquery-3.6.3.min.js'; // Check https://jquery.com/ for the current version
//document.getElementsByTagName('head')[0].appendChild(script)

class BasketManager {

    currentBasket = null;
    currentUser = {};
    current = this;
    AddProduct(productName, productId, url, quantity, price, pageId) {
        if (this.currentBasket == null) {
            var basket = localStorage.getItem("CurrentBasket");
            if (basket = null) {
                this.currentBasket = new Array();
            } else {
                this.currentBasket = JSON.parse(basket);
            }
        }

        var product = { PageId: pageId, ProductName: productName, ProductId: productId, Url: url, Quantity: 1, Price: price };
        this.currentBasket.push(product);
        localStorage.setItem("CurrentBasket", JSON.stringify(this.currentBasket));
    }

    Load(element) {
        this.AddProduct(
            element.dataset.products,
            element.dataset.productid,
            element.dataset.url,
            1,
            element.dataset.price,
            element.dataset.pageid);
        return false;
    };
}

class BasketManager {
    constructor() {
        this.currentBasket = null;
        this.currentUser = {};
    }

    AddProduct(productName, productId, url, quantity, price, pageId) {
        if (this.currentBasket == null) {
            this.currentBasket = localStorage.getItem("CurrentBasket") ? JSON.parse(localStorage.getItem("CurrentBasket")) : [];
        }

        let productExists = this.currentBasket.some(item => item.ProductId === productId);

        if (productExists) {
            // Si le produit est déjà dans le panier, augmentez sa quantité
            this.currentBasket.find(item => item.ProductId === productId).Quantity += quantity;
        } else {
            // Sinon, ajoutez un nouveau produit
            var product = { PageId: pageId, ProductName: productName, ProductId: productId, Url: url, Quantity: quantity, Price: price };
            this.currentBasket.push(product);
        }

        localStorage.setItem("CurrentBasket", JSON.stringify(this.currentBasket));
    }

    Load(element) {
        let productName = element.dataset.products;
        let productId = element.dataset.productid;
        let url = element.dataset.url;
        let quantity = element.dataset.quantity || 1; // Par défaut à 1 si non spécifié
        let price = element.dataset.price;
        let pageId = element.dataset.pageid;

        if (productName && productId && url && price && pageId) {
            this.AddProduct(productName, productId, url, parseInt(quantity), price, pageId);
        } else {
            console.error("Missing data for product addition");
        }
        return false;
    };
}


class GestionnairePanier {

    PanierCourant = null;
    currentUser = {};
    current = this;
    AddProduct(produitNom, produitId, url, quantite, prix, pageId) {
        if (this.PanierCourant == null) {
            var panier = localStorage.getItem("PanierCourant");
            if (panier = null) {
                this.PanierCourant = new Array();
            } else {
                this.PanierCourant = JSON.parse(panier);
            }
        }

        var produit = { PageId: pageId, ProduitNom: produitNom, ProduitId: produitId, Url: url, Quantite: quantite, Prix: prix };
        this.PanierCourant.push(product);
        localStorage.setItem("PanierCourant", JSON.stringify(this.PanierCourant));
    }

    Load(element) {
        this.AddProduct(
            element.dataset.produitNom,
            element.dataset.produitId,
            element.dataset.url,
            1,
            element.dataset.prix,
            element.dataset.pageid);
        return false;
    };
}