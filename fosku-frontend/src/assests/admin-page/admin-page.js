import { useState } from 'react';
import './admin-page.css'
import { backend_server_url } from '../../settings';
import { useNavigate } from 'react-router-dom';

export default function AdminPage() {
    const navigate = useNavigate();
    const [login, setLogin] = useState({});
    const submitHandler = (e) => {
        e.preventDefault()

        //TODO: add auth logic

        navigate("/admin/dashboard")
    }

    const onChangeHandler = (e) => {
        setLogin(prev => { return { ...prev, [e.target.name]: e.target.value } })
    }

    return (
        <div>
            <form onSubmit={submitHandler} method='post'>
                <h2>Log in to the CMS</h2>
                <input name='email' type="email" placeholder='Email...' value={login.email} onChange={onChangeHandler} />
                <input name='password' type='password' placeholder='Password...' value={login.password} onChange={onChangeHandler} />
                <button type='submit'>Log in</button>
            </form>
        </div>
    );
}