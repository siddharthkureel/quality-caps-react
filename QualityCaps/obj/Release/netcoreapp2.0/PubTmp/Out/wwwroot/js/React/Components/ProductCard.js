import React from 'react';

const ProductCard=(props)=>{
    return(
            <div className="card" >
                <img src={props.image} width="300"  alt="Card image cap"/>
                    <div className="card-body">
                        <h5 className="card-title">{props.name} || ${props.price}</h5>
                <p className="card-text">{props.description} || {props.category}</p>
                        <a href={`/shoppingCart/AddtoCart/` + props.id} className="btn btn-primary">
                        Add to Cart</a>
                    </div>
            </div>
    );
}
export default ProductCard;