import React, {useState} from 'react';
import Header from './Header';
import style from './Login.module.css';
import userService from '../../service/userService';

const Login: React.FC = () => {
  const [password, setPassword] = useState<string>("");
  const [email, setEmail] = useState<string>("");

  const login = async () => {
    const res = await userService.login(email, password);
    const data = res.data;
    const JWT = res.headers.JWT;
  }

  const signUp = () => {
    alert('not implemented yet');
  }

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
        <p className={style.signUpRedirect}>Don't have an account? <a onClick={signUp}>Sign Up</a></p>
      </div>
    </div>
    </>
  );
};

export default Login;
