import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import ErrorBoundary from './components/ErrorBoundary';
import Home from './pages/Home';
import Cadastro from './pages/Cadastro';
import Detalhes from './pages/Detalhes';

import './index.css';

function App() {
  return (
    <ErrorBoundary>
      <Router>
        <div className="container">
          <header className="main-header">
            <div className="logo-box" style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
              <svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round" style={{ color: 'var(--primary)' }}>
                <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"></path>
                <circle cx="12" cy="10" r="3"></circle>
              </svg>
              <span style={{ color: 'var(--text-main)' }}>Turis</span>Map
            </div>
            <Link to="/novo" className="btn-main-action">cadastrar um ponto turístico</Link>
          </header>

          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/detalhes/:id" element={<Detalhes />} />
            <Route path="/novo" element={<Cadastro />} />
            <Route path="/editar/:id" element={<Cadastro />} />
          </Routes>
        </div>
      </Router>
    </ErrorBoundary>
  );
}

export default App;