import React from "react";

const SearchBar=(props)=>{
    return(
        <div>
            <input type="text" className="form-control" placeholder="Search Caps by Name" onChange={props.onSearchChanges} />
        </div>
    );
}
export default SearchBar;