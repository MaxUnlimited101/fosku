import "./product-details-page.css";
import PageWrapper from "../page-wrapper/page-wrapper";
import { backend_server_url } from '../../settings'
import { useParams } from "react-router-dom";

export default async function ProductDetailsPage() {
    const { id } = useParams();
    // const resp = await fetch(`${backend_server_url}/product/${id}`);
    // if (!resp.ok) {
    //     throw new Error(`Response status: ${resp.status}`);
    // }
    // const product = await resp.json();
    // console.log(product);

    const product = {}

    return (
        <PageWrapper>
            <div className="product-page">
                <div className="product-image">
                    <img src={product.image} alt={product.name} />
                </div>
                <div className="product-details">
                    <h1>{product.name}</h1>
                    <p className="price">${product.price.toFixed(2)}</p>
                    <p className="description">{product.description}</p>

                    <ul className="features">
                        {product.features.map((feature, index) => (
                            <li key={index}>{feature}</li>
                        ))}
                    </ul>

                    <p className="stock-status">
                        {product.inStock ? "In Stock" : "Out of Stock"}
                    </p>

                    <button
                        className="add-to-cart-button"
                        disabled={!product.inStock}
                        onClick={() => alert(`${product.name} added to cart!`)}
                    >
                        {product.inStock ? "Add to Cart" : "Unavailable"}
                    </button>
                </div>
            </div>
        </PageWrapper>
    );
}
