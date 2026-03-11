import React, { Component } from 'react';

class ErrorBoundary extends Component {

  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.error("Erro capturado:", error, errorInfo);
  }

  render() {
    if (this.state.hasError) {

      return (
        <div style={{ padding: '20px', textAlign: 'center' }}>

          <h2>Ops! Algo deu errado no Front-end.</h2>
          <button onClick={() => window.location.reload()}>Recarregar Página</button>
          
        </div>
      );
    }

    return this.props.children; 
  }
}

export default ErrorBoundary;