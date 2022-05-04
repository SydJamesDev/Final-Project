import React from "react";
import axios from "axios";
import Collapsible from 'react-collapsible';

export default class Posts extends React.Component {
  constructor(props) {
    super(props);
    this.state = { posts: [], title:'', content:'' };

    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.getData();
    this.setState({
      posts: []
    })
  }

  handleDelete(e) {
    console.log(e.target.id)
    const postId = e.target.id;
    axios.delete('http://localhost:5000/api/posts/'+ postId)
    .then(res => {
      console.log(res);
    })
    window.location.reload(false);
  }

  handleInputChange(e) {
    const target = e.target;
    const value = target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  }

  handleSubmit(e) {
    e.preventDefault();
    const post = {title: this.state.title, content: this.state.content};
    console.log(post);
    axios.post('http://localhost:5000/api/posts', post)
      .then(res =>{
        console.log(res);
      })
    window.location.reload(false);
  }

  getData = () => {
    axios.get('http://localhost:5000/api/posts').then(response => this.setState({posts: response.data}));
  }

  render() {
    return (
      <div id="MainPage">
      <h1>List of Posts</h1>
      <ul>
          { this.state.posts.map(post => 
          <li key={post.id}>
          <p>{post.title}</p>
            <Collapsible trigger={<button id="expand">Expand Post</button>}>
              <p>{post.content}</p>
            </Collapsible>
            <button id={post.id} onClick={this.handleDelete}>Delete Post</button>
          </li>)}
        </ul>
        <form onSubmit={this.handleSubmit}>
            <label>
              Post Title: 
              <input type="text" name="title" value={this.state.title} onChange={this.handleInputChange} />
            </label>
            <label>
              Post Content: 
              <textarea name="content" value={this.state.content} onChange={this.handleInputChange} />
            </label>
            <input type="submit" value="Submit"/>
        </form>
      </div>
    )
  }
}