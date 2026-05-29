import { Stack, Table } from "@chakra-ui/react";
import useUsers from "./useUsers";

type Props = {};

function StudentTable({}: Props) {
  const { data, isLoading, error, isError } = useUsers();

  console.log(data);

  if (isLoading) return <div>Loading users...</div>;

  if (isError) {
    return <div className="text-danger">Error: {error?.message}</div>;
  }

  return (
    <Stack gap="10">
      <Table.Root>
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader>Id</Table.ColumnHeader>
            <Table.ColumnHeader>Name</Table.ColumnHeader>
            <Table.ColumnHeader>Last Name</Table.ColumnHeader>
            <Table.ColumnHeader>Email</Table.ColumnHeader>
            <Table.ColumnHeader>UserType</Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {data?.map((user) => (
            <Table.Row key={user.id}>
              <Table.Cell>{user.id}</Table.Cell>
              <Table.Cell>{user.firstName}</Table.Cell>
              <Table.Cell>{user.lastName}</Table.Cell>
              <Table.Cell>{user.email}</Table.Cell>
              <Table.Cell>{user.userType}</Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </Stack>
  );
}

export default StudentTable;
