interface CaseCardProps {
  title: string;
  victimName: string;
  onClick: () => void;
}

export function CaseCard({ title, victimName, onClick }: CaseCardProps) {
  return (
    <button
      onClick={onClick}
      className="bg-[#ece6d6] px-5 py-4 text-left hover:scale-[1.02] transition-transform shadow-lg rounded-sm"
    >
      <div className="text-[#12141c] font-bold text-lg">{title}</div>
      <div
        className="text-[#a9762f] text-xs mt-1"
        style={{ fontFamily: "'IBM Plex Mono', monospace" }}
      >
        Victim: {victimName}
      </div>
    </button>
  );
}
