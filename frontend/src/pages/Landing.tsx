import { useNavigate } from "react-router-dom";

export function Landing() {
  const navigate = useNavigate();

  return (
    <div
      className="min-h-screen flex flex-col items-center justify-center px-6 relative overflow-hidden"
      style={{
        background:
          "radial-gradient(ellipse 600px 400px at 50% 0%, rgba(232,163,61,0.14), transparent 70%), #12141c",
      }}
    >
      <div className="text-center max-w-lg">
        <div
          className="text-[#a9762f] text-xs tracking-[0.3em] uppercase mb-6"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          A Coding Murder Mystery
        </div>

        <h1
          className="text-[#f0ede3] text-5xl mb-4"
          style={{ fontFamily: "'Special Elite', cursive" }}
        >
          Case File
        </h1>

        <p className="text-[#8b91a7] text-base mb-12 leading-relaxed">
          Every case starts with a body and a broken codebase. Only you can read
          both.
        </p>

        <button
          onClick={() => navigate("/cases")}
          className="w-24 h-24 rounded-full bg-[#e8a33d] text-[#12141c] font-bold text-sm tracking-wide hover:scale-105 transition-transform shadow-lg"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          PLAY
        </button>
      </div>
    </div>
  );
}
