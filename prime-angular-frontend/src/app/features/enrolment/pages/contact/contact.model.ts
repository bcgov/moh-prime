export interface Contact {
  preferred_contact_method: {
    prefers_email: boolean;
    email: string;
    prefers_phone: boolean;
    phone: string;
  };
  voice_contact: {
    phone: string;
    ext: number;
  };
}
