import type { Suspect } from "./suspect";

export interface Case {
  id: string;
  title: string;
  introText: string;
  victimName: string;
  suspects: Suspect[];
}
