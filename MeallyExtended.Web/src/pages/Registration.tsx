import React, { useState } from "react";
import { useRegister } from "@/hooks/useRegister";

function RegisterForm() {
  const { register, loading, error, response } = useRegister();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    await register(email, password);
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit" disabled={loading}>
          Register
        </button>
      </form>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      {response && <p>Success: {JSON.stringify(response)}</p>}
    </div>
  );
}

export default RegisterForm;
