import React from "react";
import ProductCard from "./ProductCard";
const Product=({caps})=>{
    const data = caps.map((cap)=>{
        return <ProductCard 
        key={cap.id}
        id={cap.id}
        name={cap.name} 
        price={cap.price}
        description={cap.description}
        image={cap.image}
        category={cap.categoryID} />
    });
    return <div className='align'>{data}</div>
}
export default Product;