import { useState } from "react";

interface PlayerNameFormProps {
  onSubmit: (name: string) => void;
  submitting: boolean;
}

export function PlayerNameForm({ onSubmit, submitting }: PlayerNameFormProps) {
  const [nameInput, setNameInput] = useState("");

  return (
    <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-6">
      <h1
        className="text-[#f0ede3] text-2xl"
        style={{ fontFamily: "'Special Elite', cursive" }}
      >
        Who's investigating?
      </h1>
      <input
        type="text"
        value={nameInput}
        onChange={(e) => setNameInput(e.target.value)}
        placeholder="Detective's name"
        className="bg-[#ece6d6] text-[#12141c] px-4 py-3 rounded-sm w-full max-w-xs text-center"
        style={{ fontFamily: "'IBM Plex Mono', monospace" }}
      />
      <button
        onClick={() => onSubmit(nameInput.trim())}
        disabled={!nameInput.trim() || submitting}
        className="px-6 py-3 bg-[#e8a33d] text-[#12141c] font-bold disabled:opacity-40"
        style={{ fontFamily: "'IBM Plex Mono', monospace" }}
      >
        {submitting ? "..." : "Continue"}
      </button>
    </div>
  );
}
