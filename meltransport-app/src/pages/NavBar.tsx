import {
  Box,
  Flex,
  HStack,
  IconButton,
  Stack,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import type { ReactNode } from "react";
import { NavLink as RouterLink } from "react-router-dom";

import { GiHamburgerMenu } from "react-icons/gi";
import { IoMdClose } from "react-icons/io";

const Links: Array<{ uri: string; label: string }> = [
  { uri: "/", label: "Home" },
  { uri: "/students", label: "Students" },
];

const NavLink = ({ children, to }: { children: ReactNode; to: string }) => (
  <Text
    px={2}
    py={1}
    rounded={"md"}
    _hover={{
      textDecoration: "none",
      color: "fg.default",
      bg: "bg.canvas",
    }}
  >
    <RouterLink to={to}>{children}</RouterLink>
  </Text>
);

export default function NavBar() {
  const { open, onOpen, onClose } = useDisclosure();

  return (
    <>
      <Box color="fg.default" bg="bg.canvas" px={4}>
        <Flex h={16} alignItems={"center"} justifyContent={"space-between"}>
          <IconButton
            size={"md"}
            aria-label={"Open Menu"}
            display={{ md: "none" }}
            onClick={open ? onClose : onOpen}
          >
            {open ? <IoMdClose /> : <GiHamburgerMenu />}
          </IconButton>
          <HStack margin={8} alignItems={"center"}>
            <HStack
              as={"nav"}
              margin={4}
              display={{ base: "none", md: "flex" }}
            >
              {Links.map((link) => (
                <NavLink to={link.uri} key={link.uri}>
                  {link.label}
                </NavLink>
              ))}
            </HStack>
          </HStack>
        </Flex>

        {open ? (
          <Box pb={4} display={{ md: "none" }}>
            <Stack as={"nav"} margin={4}>
              {Links.map((link) => (
                <NavLink to={link.uri} key={link.uri}>
                  {link.label}
                </NavLink>
              ))}
            </Stack>
          </Box>
        ) : null}
      </Box>
    </>
  );
}
