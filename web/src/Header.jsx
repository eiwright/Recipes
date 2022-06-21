
import React from 'react';
import styled from 'styled-components';

const Headr = styled.header`
    background: #7c7fc4;
    padding: 30px 0;
    color: #fff;
`;
const HeaderTitle = styled.h1`
    font-weight: 400;
    margin: 0;
    color:#e8d76c;
`;
const HeaderBar = styled.div`
    align-middle;
    align-justify;
`;
const Header = () => {
	return(
		<Headr>
			<HeaderBar>
				<HeaderTitle>Your Recipes</HeaderTitle>
			</HeaderBar>
		</Headr>
	);
};

export default Header;