import  panda from '../assets/panda.png';
import style from './Navbar.module.css';

const Navbar:React.FC = () => {
  return (
    <header>
        <nav>
          <div className={style.logo}>
            <img src={panda} alt='panda' className={style.panda}></img>
            <h1 className={style.pandaLogo}>FoodPanda</h1>
          </div>
        </nav>
    </header>
  );
}

export default Navbar;
