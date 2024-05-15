import useAuthenticate from '@/hooks/authenticate';
import React, { useState } from 'react';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { authenticateUser, loading, error } = useAuthenticate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    await authenticateUser(email, password);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  else if (error) {
    return <div>Error: {error}</div>;
  }

  else {
    return <div>Welcome, User!</div>;
  }

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Email:
        <input type="email" value={email} onChange={e => setEmail(e.target.value)} required />
      </label>
      <label>
        Password:
        <input type="password" value={password} onChange={e => setPassword(e.target.value)} required />
      </label>
      <button type="submit">Log in</button>
    </form>
  );
};

export default LoginPage;