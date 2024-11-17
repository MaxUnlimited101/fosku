import { Link } from 'react-router-dom';
import './admin-products-page.css'
import { backend_server_url } from '../../settings';
import { useState } from 'react';
import useSWR from 'swr';

export function AdminPageProductComponent({ product }) {
    const [productChanged, setProduct] = useState(product);
    const onChangeHandler = (e) => {
        setProduct(prev => { return { ...prev, [e.target.name]: e.target.value } })
    }

    return (
        <div>
            <input type='text' disabled='true' value={productChanged.id} />
            <input name='name' type='string' value={productChanged.name} onChange={onChangeHandler} />
            <input name='description' type='string' value={productChanged.description} onChange={onChangeHandler} />
            <input name='price' type='string' value={productChanged.price} onChange={onChangeHandler} />
            <input name='stockQuantity' type='string' value={productChanged.stockQuantity} onChange={onChangeHandler} />
        </div>
    );
}

const fetcher = (...args) => fetch(...args).then((res) => res.json());

export default async function AdminProductsPage() {
    const {
        data: products,
        error,
        isValidating,
    } = useSWR(`${backend_server_url}/products`, fetcher);

    console.log(products)

    if (error) {
        <div>
            <h3>Server is down!</h3>
        </div>
    }
    else if (isValidating) {
        return (
            <div>
                <p>Loading...</p>
            </div>
        );
    }

    return (
        <div>
            <Link to={"/admin/product/create"}>Create new product</Link>
            <div>
                {products && products.forEach(element => {
                    <AdminPageProductComponent product={element} />
                })}
            </div>
        </div>
    );
}