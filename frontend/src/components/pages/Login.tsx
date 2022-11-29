
import React, {useState} from 'react';
import Header from './Header';
import style from './Login.module.css';
import userService from '../../service/userService';
import { Link, useNavigate } from 'react-router-dom';
import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Login: React.FC = () => {
  const [password, setPassword] = useState<string>("");
  const [email, setEmail] = useState<string>("");

  const navigate = useNavigate();

  const login = async () => {
    const res = await userService.login(email, password);
    const data = res.data; //user
    const JWT = res.headers.token; //token
    console.log(JWT)
    sessionStorage.setItem('user', JSON.stringify(data));
    if(JWT != undefined){
      sessionStorage.setItem('token', JWT);
    }
    navigate('/app');
  }

  const startPage = () => {
    navigate('/start');
  };


  return (
    <>
    <Header />
    <div className={style.container}>
      <div className={style.loginPage}>
      <h1 className={style.loginTitle}>Log in</h1>
        <form className={style.loginFormContainer}>
          <input
            type="email"
            name="email"
            id="email"
            className={style.loginInput}
            placeholder="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type="password"
            name="password"
            id="password"
            className={style.loginInput}
            placeholder="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </form>
        <button className={style.loginButton} onClick={login}>Log in</button>
        <p className={style.signUpRedirect}>
            Don't have an account?{' '}
            <Link to={'/signup'}>
              <a>Sign Up</a>
            </Link>
          </p>
      </div>
      </div>
    </>
  );
};

export default Login;
