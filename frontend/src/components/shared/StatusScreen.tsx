interface StatusScreenProps {
  variant: "error" | "loading";
  message: string;
}

export function StatusScreen({ variant, message }: StatusScreenProps) {
  const textColor = variant === "error" ? "text-red-400" : "text-white";

  return (
    <div
      className={`min-h-screen bg-[#12141c] ${textColor} flex items-center justify-center`}
    >
      {message}
    </div>
  );
}
