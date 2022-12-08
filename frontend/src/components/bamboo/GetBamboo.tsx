import { join } from 'path';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Index from '..';
import BambooService from '../../service/bambooService';
import RecipeService from '../../service/recipeService';
import UserService from '../../service/userService';
import { BambooSession, Recipe, Seat, User } from '../../types';
import style from '../recipe/GetRecipe.module.css';

const GetBamboo: React.FC = () => {
  const [recipe, setRecipe] = useState<Recipe>();
  const [author, setAuthor] = useState<User>();
  const [bamboo, setBamboo] = useState<BambooSession>();
  const [seats, setSeats] = useState<Seat[]>();
  const [emptySeats, setEmptySeats] = useState<number>(0);

  const navigate = useNavigate();

  const getData = async () => {
    const response = await BambooService.getBambooSession(window.location.pathname.split('/')[2]);
    const data = response.data;
    setBamboo(data);
    getRecipe(data.recipe);
    setSeats(data.seats);
    getEmptySeats(data.seats);
  };

  const getRecipe = async (id: string) => {
    const response = await RecipeService.getRecipe(id);
    const data = response.data;
    setRecipe(data);
    getAuthor(data.author);
  };

  const getAuthor = async (id: string) => {
    const response = await UserService.getUser(id);
    const data = response.data;
    setAuthor(data);
  };

  const deleteBamboo = async () => {
    if (bamboo && confirm('Are you sure you want to delete this recipe?')) {
      //const response = await BambooService.deleteBambooSession(bamboo?.sessionId);
      alert("don't delete yet pls :)");
      navigate('/bamboosessions');
    }
  };

  const join = async (seat: Seat) => {
    if (bamboo && confirm('Are you sure you want to join this session?')) {
      const response = await BambooService.joinBambooSession(bamboo.sessionId, seat.seatId);
      if (response.status === 200 && response.data == true) {
        alert('You have successfully joined this session!');
        window.location.reload();
      }
      if(response.status == 200 && response.date == false){
        alert("Seat has been taken");
        window.location.reload();
      }
    }
  };

  const getEmptySeats = (seats: Seat[]) => {
    let emptySeats = seats.length;
    seats?.forEach(seat => {
      if (seat.user) {
        emptySeats--;
      }
    });
    setEmptySeats(emptySeats);
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
    <div className={style.recipeContent}>
      <div className={style.recipeImg}>
        <img src={recipe?.pictureURL} alt="" />
      </div>
      <h6>recipe author: {author?.firstName + " " + author?.lastName}</h6>
      <h1>Bamboo Session: {recipe?.name}</h1>
      <div className={style.container}>
        <h4>organized by: {author?.firstName + " " + author?.lastName}</h4>
        <h5>description: </h5>
        <p>{bamboo?.description}</p>
        <h5>Where?</h5>
        <p>{bamboo?.address}</p>
        <h5>total slots: {bamboo?.slotsNumber}</h5>
        <h5>available seats: {emptySeats}</h5> {/* TODO: implement this*/}
      <div className={style.concurrencyContainer}>
        {seats?.map((seat, Index)  => (
          <div key={seat.seatId} className={style.seat}>
            <h5>seat {Index+1}: </h5>
            {seat.user ? <p className={style.reservedOrBtn}>reserved by: {seat.user.firstName + " " + seat.user.lastName}</p> : <button onClick={() => join(seat)} className={style.reservedOrBtn} id={style.joinBtn}>join</button>}
          </div>
          ))}
      </div>
      </div>
      <div className={style.buttonContainer}>
        <button onClick={(e) => deleteBamboo()} className={style.deleteBtn}>delete</button>
      </div>
    </div>
    </>
  );
};

export default GetBamboo;
