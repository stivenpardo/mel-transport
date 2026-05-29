import { useQuery } from "@tanstack/react-query";
import axios from "axios";

export type User = {
    id: string;
    firstName: string;
    lastName: string;
    email?: string;
    userType: number;
}

export default function useUsers() {
    return useQuery({
        queryKey: ["users"],
        queryFn: () => 
            axios.get<User[]>("http://localhost:5000/users")
            .then(response => response.data)
    })
}