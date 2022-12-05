import { abort } from 'process';
import React, { useEffect, useState } from 'react'
import { Link, Navigate, useNavigate } from 'react-router-dom';
import BambooService from '../../service/bambooService';
import { BambooSession, Recipe } from '../../types';
import style from './GetBamboos.module.css';

const GetBamboos: React.FC = ()=> {

  const [bamboos, setBamboos] = useState<any>([]);

const navigate = useNavigate();

  const getData = async() => {
    const response = await BambooService.getBambooSessions();
    const data = response.data;
    setBamboos(data);
  };

  useEffect(() => {
    getData();
    console.log(bamboos);
  }, [])

  return (
    <>
      <div className={style.pageContent}>
        <h2 style={{color: '#DCBEA8',}}>explore</h2>
        {bamboos.map((bamboo: BambooSession) => (
          <div className={style.recipe} key={bamboo.SessionId + bamboo.Host} id={style.recipeChild} onClick={() => navigate('/bamboosession/' + bamboo.SessionId)}>
            <div>{bamboo.SessionId}</div>
          </div>
        ))}
      </div>
    </>
  )
}

export default GetBamboos
