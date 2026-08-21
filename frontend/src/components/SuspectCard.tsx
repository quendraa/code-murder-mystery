interface SuspectCardProps {
  name: string;
  role: string;
  onClick?: () => void;
  selected?: boolean;
}

function SuspectCard({ name, role, onClick, selected }: SuspectCardProps) {
  const initials = name
    .split(" ")
    .map((part) => part[0])
    .join("")
    .toUpperCase();

  return (
    <div
      onClick={onClick}
      className={`bg-[#ece6d6] px-4 py-5 flex flex-col items-center text-center gap-2 max-w-180 rounded-sm shadow-lg ${
        onClick ? "cursor-pointer" : ""
      }`}
      style={{
        border: selected ? "2px solid #b5432f" : "2px solid transparent",
      }}
    >
      <div className="w-10 h-10 rounded-full bg-[#12141c]/10 flex items-center justify-center text-lg font-bold text-[#12141c]">
        {initials}
      </div>
      <div className="text-[#12141c] text-sm font-bold">{name}</div>
      <div className="text-[#a9762f] text-xs font-mono">{role}</div>
    </div>
  );
}

export default SuspectCard;
