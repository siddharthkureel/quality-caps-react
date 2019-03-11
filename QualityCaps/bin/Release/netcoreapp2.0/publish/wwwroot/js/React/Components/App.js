import React from "react";
import Product from "./Product";
import axios from 'axios';
import SearchBar from "./SearchBar";
class App extends React.Component{  
    constructor(props){
        super(props);
        this.state={
            caps:[],
            searchField:''
        };
        this.onSearchChanges = this.onSearchChanges.bind(this);
        this.priceSort = this.priceSort.bind(this);
        this.nameSort = this.nameSort.bind(this);
    }
    
    componentDidMount (){
        axios.get("http://joykureel-001-site1.ctempurl.com/api/Capsapi").then(response => {
            //console.log(response.data);  
            this.setState({
                caps: response.data
            });
        });
    } 
    onSearchChanges(event) {
        this.setState({searchField:event.target.value});
    }
    priceSort(){
        this.setState({ caps: this.state.caps.reverse((a, b) => parseFloat(a.price) - parseFloat(b.price))});
    }
    nameSort(){
        this.setState({caps:this.state.caps.reverse()});
    }
  
    
    render(){
        const { caps, searchField } = this.state;
        const filterArray = caps.filter(cap => {
            return cap.name.toLowerCase().includes(searchField.toLowerCase());
        });
        return(
            <div>
                <div className="price">
                    <div>Price:
                         <span className="up" onClick={this.priceSort}> &#8593;&darr; </span>
                    </div>
                    <div>Name:
                         <span className="up" onClick={this.nameSort}> &#8593;&darr;</span>
                    </div>
                    <SearchBar onSearchChanges={this.onSearchChanges} />
                </div>
                
                <br />                
               <Product caps={filterArray}/>
            </div>
        );
    }
}
export default App;