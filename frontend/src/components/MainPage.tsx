import appdemo from '../assets/appDemo.png';
import style from './MainPage.module.css';

const Main:React.FC = () => {
  return (
    <div className={style.main}>
      <div className={style.main_container}>
        <h1>Tinder for food</h1>
        <p>swipe right on your favorite food</p>
        <p>try out new attractive recipes</p>
        <h3>launching 1/2023</h3>
      </div>
      <img src={appdemo} alt="appdemo" className={style.appDemo}></img>
    </div>
  );
}

export default Main;
