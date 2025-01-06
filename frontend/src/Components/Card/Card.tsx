import React from 'react';
import './Card.css';

type Props = {};

const Card = (props: Props) => {
  return (
    <div className="card">
      <img
        src="https://images.unsplash.com/photo-1566045936442-0393604c3c4d?q=80&w=1966&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="image"
      ></img>
      <div className="details">
        <h2>AAPL</h2>
        <p>$110</p>
        <p className="info">
          {' '}
          Lorem ipsum dolor sit, amet consectetur adipisicing elit. Odio,
          molestias!
        </p>
      </div>
    </div>
  );
};

export default Card;
